using Carter;
using MediatR;
namespace netcoreTemplate.api.Modules;

public class apiModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/",async (HttpContext _, IMediator mediator, CancellationToken ct)=>{

            await mediator.Send(new {},ct);
        });
    }
}
