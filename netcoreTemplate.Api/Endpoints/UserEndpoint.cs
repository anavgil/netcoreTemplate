using Application.Authentication.Login;
using Application.Users.Login;
using Application.Users.Register;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

/// <summary>
/// 
/// </summary>
public class UserEndpoint : IEndpoint
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

        RouteGroupBuilder group = app.MapGroup("v{version:apiVersion}/user")
                                    .AllowAnonymous()
                                    .WithApiVersionSet(apiVersionSet)
                                    .WithTags("Users");

        group.MapPost("/login", async ([FromBody] LoginRequestDto dto, ISender mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new LoginUserCommand(dto), ct);

            return TypedResults.Ok(result.Value);
            //if (result.IsSuccess)
            //{
            //    return TypedResults.Ok(result.Value);
            //}
            //else
            //{
            //    return TypedResults.NotFound();
            //}
        });

        group.MapPost("/register", async ([FromBody] RegisterRequestDto request, ISender mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new RegisterUserCommand(request), ct);
            return TypedResults.Ok(result.Value);

        });
    }
}
