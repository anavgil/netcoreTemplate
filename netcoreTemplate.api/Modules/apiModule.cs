using Carter;

namespace netcoreTemplate.api.Modules;

public class apiModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/",()=>{

        });
    }
}
