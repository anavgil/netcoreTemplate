using System.Text.Json;
using System.Text.Json.Serialization;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors;

public sealed class RequestResponseLoggingBehavior<TRequest, TResponse>(ILogger<RequestResponseLoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var correlationId = Guid.NewGuid();

        var t = new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        // Request Logging
        // Serialize the request
        var requestJson = JsonSerializer.Serialize(request,t);
        // Log the serialized request
        logger.LogInformation("Handling request {CorrelationID}: {Request}", correlationId, requestJson);

        // Response logging
        var response = await next();
        // Serialize the request
        try
        {
            var responseJson = JsonSerializer.Serialize(response, t);

            // Log the serialized request
            logger.LogInformation("Response for {Correlation}: {Response}", correlationId, responseJson);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error serializing response for {Correlation}",correlationId);
        }

        // Return response
        return response;
    }
}
