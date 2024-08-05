
using MediatR;
using netcoreTemplate.application.CQRS.Querys;

namespace netcoreTemplate.Api.Modules;

public class EndpointTest : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("testApi");

        group.WithTags("TestApi");

        group.MapGet("/test", () => Results.Ok());

        group.MapGet("/", async (HttpContext _, ISender mediator, CancellationToken ct) =>
        {

            await mediator.Send(new TestQueryRequestRequest(), ct);
        });

        group.MapGet("/{id}", async (HttpContext _, string id, ISender mediator, CancellationToken ct) =>
        {
            if (string.IsNullOrWhiteSpace(id))
                return Results.BadRequest();

            if (!Guid.TryParse(id, out var id2))
            {
                return Results.BadRequest();
            }

            var request = new TestQueryParamRequestRequest(id2);
            var result = await mediator.Send(request, ct);

            return Results.Ok(result);
        });
    }
}
