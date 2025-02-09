
using Application.Users.Dtos;
using Application.Users.Login;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
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

        group.MapPost("/login", DoLogin);
    }

    private static async Task<Results<Ok<LoginResponseDto>, NotFound>> DoLogin(HttpContext _, [FromBody] LoginRequestDto dto, ISender mediator, CancellationToken ct)
    {
        var result = await mediator.Send(new LoginUserCommand(dto), ct);

        if (result.IsSuccess)
        {
            return TypedResults.Ok(result.Value);
        }
        else
        {
            return TypedResults.NotFound();
        }

    }
}
