using Carter;
using MediatR;
using netcoreTemplate.application.CQRS.Querys;

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
            var request = new TestQueryParamRequestRequest(Guid.Parse(id));
            await mediator.Send(request, ct);
        });
    }
}
