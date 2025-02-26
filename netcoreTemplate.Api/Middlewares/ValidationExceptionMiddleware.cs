// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Api.Middlewares;

/// <summary>
/// 
/// </summary>
/// <param name="next"></param>
public sealed class ValidationExceptionMiddleware(RequestDelegate next)
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException exception)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "ValidationFailure",
                Title = "Validation error",
                Detail = "One or more validation errors has occurred"
            };

            if (exception.Errors is not null)
            {
                problemDetails.Extensions["errors"] = exception.Errors;
            }

            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
