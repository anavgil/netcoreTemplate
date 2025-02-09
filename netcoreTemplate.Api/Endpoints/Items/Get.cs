using Api.Extensions;
using Application.Items.Dtos;
using Application.Items.Get;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;

namespace Api.Endpoints.Items;

internal sealed class Get : IEndpoint
{

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
        //.RequireAuthorization()
        .WithDescription("Get all items")
        .WithSummary("Get all items")
        .MapToApiVersion(1);

        group.MapGet("", async (HttpContext _, ISender mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new GetItemsQuery(), ct);

            return result.Match(onSuccess: Results.Ok, onFailure: Results.NotFound);

        })
        //.RequireAuthorization()
        .WithDescription("Get all itemsV2")
        .WithSummary("Get all itemsV2")
        .MapToApiVersion(2);
    }
}
