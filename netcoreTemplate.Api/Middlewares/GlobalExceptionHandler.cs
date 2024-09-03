using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Middlewares;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> _logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var exceptionMessage = exception.Message;
        var problemDetails = new ProblemDetails()
        {
            Instance = httpContext.Request.Path,
            Status = GetStatuscodeFromException(exception)
        };

        _logger.LogError(exception,
            "Error Message: {}, Time  of occurrence {time}", exceptionMessage, DateTime.UtcNow);

        if (exception is ValidationException fluentException)
        {
            problemDetails.Title = "one or more validation errors occurred.";
            problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
            List<string> validationErrors = [];
            foreach (var error in fluentException.Errors)
            {
                validationErrors.Add(error.ErrorMessage);
            }
            problemDetails.Extensions.Add("errors", validationErrors);
        }
        else
        {
            problemDetails = new ProblemDetails
            {
                Title = exception.GetType().Name,
                Detail = exception.Message,
                Type = exception.GetType().Name
            };

            if (exception.InnerException is not null)
            {
                problemDetails.Extensions = new Dictionary<string, object>()
            {
                { "INNER-Message",exception.InnerException.Message },
                { "INNER-Type",exception.InnerException.GetType().Name}
            };
            }
        }

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;

    }

    private int GetStatuscodeFromException(Exception exception) => exception switch
    {
        BadHttpRequestException => StatusCodes.Status400BadRequest,
        ValidationException => StatusCodes.Status400BadRequest,
        _ => StatusCodes.Status500InternalServerError
    };
}
