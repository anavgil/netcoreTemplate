using Api.Extensions;
using Application.Items.Get;
using Application.Items.GetById;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Endpoints;

/// <summary>
/// 
/// </summary>
public class ItemsEndpoint : IEndpoint
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    public void MapEndpoint(IEndpointRouteBuilder app)
    {

        ApiVersionSet apiVersionSet = app.NewApiVersionSet()
                                        .HasApiVersion(new ApiVersion(1))
                                        .HasApiVersion(new ApiVersion(2))
                                        .ReportApiVersions()
                                        .Build();

        RouteGroupBuilder group = app.MapGroup("v{version:apiVersion}/items")
                                    .WithApiVersionSet(apiVersionSet)
                                    .WithTags("Items");

        group.MapGet("", async (HttpContext _, ISender mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new TestQueryRequestRequest(), ct);

            return result.Match(onSuccess: Results.Ok, onFailure: Results.NotFound);

        })
        .RequireAuthorization()
        .Produces<IReadOnlyCollection<TestQueryDto>>()
        .WithDescription("Get all items")
        .WithSummary("Get all items")
        .MapToApiVersion(1);


        group.MapGet("/{id:guid}", GetResourceById)
                .Produces<IReadOnlyCollection<TestQueryDto>>()
                .ProducesValidationProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithDescription("Get a item by Id")
                .WithSummary("Get a item by Id")
                .MapToApiVersion(1);


        group.MapGet("", async (HttpContext _, ISender mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new TestQueryRequestRequest(), ct);

            return TypedResults.Ok(result.Value);
        })
        .Produces<IReadOnlyCollection<TestQueryDto>>()
        .WithDescription("Get all items V2")
        .WithSummary("Get all items V2")
        .MapToApiVersion(2);


        static async Task<Results<Ok<IReadOnlyCollection<TestQueryDto>>, ValidationProblem, NotFound>> GetResourceById(HttpContext _, string id, ISender mediator, CancellationToken ct)
        {
            var request = new TestQueryParamRequestRequest(id);
            var result = await mediator.Send(request, ct);

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
}
