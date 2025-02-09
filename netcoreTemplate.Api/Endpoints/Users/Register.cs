
using Application.Users.Dtos;
using Application.Users.Register;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.Users;

/// <summary>
/// 
/// </summary>
public class Register : IEndpoint
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

        group.MapPost("/register", async ([FromBody] RegisterRequestDto request, ISender mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new RegisterUserCommand(request), ct);
            return TypedResults.Ok(result.Value);

        });
    }
}
