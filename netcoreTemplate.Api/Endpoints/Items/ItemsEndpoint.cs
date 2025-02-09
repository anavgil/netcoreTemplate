using Api.Extensions;
using Application.Items.Get;
using Application.Items.GetById;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;

namespace Api.Endpoints.Items;

/// <summary>
/// 
/// </summary>
public class ItemsEndpoint //: IEndpoint
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
                                    .WithTags(Tags.Items);

        group.MapGet("", async (HttpContext _, ISender mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new GetItemsQuery(), ct);

            return result.Match(onSuccess: Results.Ok, onFailure: Results.NotFound);

        })
        .RequireAuthorization()
        .Produces<IReadOnlyCollection<GetItemByIdQuery>>()
        .WithDescription("Get all items")
        .WithSummary("Get all items")
        .MapToApiVersion(1);

        group.MapGet("/{id:guid}", async (HttpContext _, Guid id, ISender mediator, CancellationToken ct) =>
        {
            var request = new GetItemByIdQuery(id);
            var result = await mediator.Send(request, ct);
            return result.Match(onSuccess: Results.Ok, onFailure: Results.BadRequest);
        })
        .Produces<IReadOnlyCollection<GetItemByIdQuery>>()
        .ProducesValidationProblem(StatusCodes.Status400BadRequest)
        //.ProducesProblem(StatusCodes.Status404NotFound)
        .WithDescription("Get a item by Id")
        .WithSummary("Get a item by Id")
        .MapToApiVersion(1);

        group.MapGet("", async (HttpContext _, ISender mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new GetItemsQuery(), ct);

            return TypedResults.Ok(result.Value);
        })
        .Produces<IReadOnlyCollection<GetItemByIdQuery>>()
        .WithDescription("Get all items V2")
        .WithSummary("Get all items V2")
        .MapToApiVersion(2);

    }
}
