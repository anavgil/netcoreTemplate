// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Api.Extensions;
using Application.Users.Dtos;
using Application.Users.Login;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Users;

/// <summary>
///
/// </summary>
public class Login : IEndpoint
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="app"></param>
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        ApiVersionSet apiVersionSet = app.NewApiVersionSet()
                .HasApiVersion(new ApiVersion(1))
                .ReportApiVersions()
                .Build();

        RouteGroupBuilder group = app.MapGroup("v{version:apiVersion}/users")
                                    .AllowAnonymous()
                                    .WithApiVersionSet(apiVersionSet)
                                    .WithTags(Tags.Users);

        group.MapPost("/login", async (HttpContext _, [FromBody] LoginRequestDto dto, ISender mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new LoginUserCommand(dto), ct);

            return result.Match(
                    onSuccess: (success) => Results.Ok(success),
                    onFailure: (error) => Results.Unauthorized());
        })
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesValidationProblem();
    }
}
