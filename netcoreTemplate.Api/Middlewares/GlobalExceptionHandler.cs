// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Middlewares;

/// <summary>
///
/// </summary>
/// <param name="problemDetailsService"></param>
/// <param name="_logger"></param>
public sealed class GlobalExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<GlobalExceptionHandler> _logger) : IExceptionHandler
{
    private const string StandarExceptionTitle = "One error occurred.";

    /// <summary>
    ///
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="exception"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var exceptionMessage = exception.Message;
        var problemDetails = new ProblemDetails()
        {
            Status = GetStatuscodeFromException(exception),
            Detail = exception.Message,
        };

        _logger.LogError(exception,
            "Error Message: {}, Time  of occurrence {time}", exceptionMessage, DateTime.UtcNow);

        problemDetails.Title = StandarExceptionTitle;
        problemDetails.Type = exception.GetType().Name;

        if (exception.InnerException is not null)
        {
            problemDetails.Extensions = new Dictionary<string, object>()
            {
                { "INNER-Message",exception.InnerException.Message },
                { "INNER-Type",exception.InnerException.GetType().Name}
            };
        }

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            Exception = exception,
            HttpContext = httpContext,
            ProblemDetails = problemDetails
        });
    }

    private static int GetStatuscodeFromException(Exception exception) => exception switch
    {
        ArgumentException => StatusCodes.Status400BadRequest,
        BadHttpRequestException => StatusCodes.Status400BadRequest,
        _ => StatusCodes.Status500InternalServerError
    };
}
