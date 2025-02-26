using Api.Extensions;
using Application.Items.Dtos;
using Application.Items.GetById;
using Asp.Versioning;
using Asp.Versioning.Builder;
using MediatR;

namespace Api.Endpoints.Items;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        ApiVersionSet apiVersionSet = app.NewApiVersionSet()
                                .HasApiVersion(new ApiVersion(1))
                                .ReportApiVersions()
                                .Build();

        RouteGroupBuilder group = app.MapGroup("v{version:apiVersion}/items")
                                    .WithApiVersionSet(apiVersionSet)
                                    .WithTags(Tags.Items);

        group.MapGet("/{id:guid}", async (HttpContext _, Guid id, ISender mediator, CancellationToken ct) =>
        {
            var request = new GetItemByIdQuery(id);
            var result = await mediator.Send(request, ct);

            return result.Match(
                onSuccess: (success) => Results.Ok(success),
                onFailure: (error) => Results.NotFound());
        })
        .Produces<IReadOnlyCollection<ItemResponseDto>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithDescription("Get a item by Id")
        .WithSummary("Get a item by Id");
    }
}
