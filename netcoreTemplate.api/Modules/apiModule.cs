using Carter;
using Carter.ModelBinding;
using MediatR;
using netcoreTemplate.application.CQRS.Querys;
using System.Net;

namespace netcoreTemplate.api.Modules;

public class apiModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (HttpContext _, IMediator mediator, CancellationToken ct) =>
        {

            await mediator.Send(new TestQueryRequestRequest(), ct);
        });

        app.MapGet("/{id}", async (HttpContext _, string id, IMediator mediator, CancellationToken ct) =>
        {
            if (string.IsNullOrWhiteSpace(id))
                return Results.BadRequest();

            if(!Guid.TryParse(id, out var id2))
            {
                return Results.BadRequest();
            }

            var request = new TestQueryParamRequestRequest(id2);
            var result = await mediator.Send(request, ct);

            return Results.Ok(result);
        });
    }
}
