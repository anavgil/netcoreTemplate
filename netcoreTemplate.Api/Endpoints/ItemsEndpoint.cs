using Application.Items.Get;
using Application.Items.GetById;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Endpoints;

public class ItemsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var apiVersion = app.NewVersionedApi();
        var group = apiVersion.MapGroup("testApi")
            .HasApiVersion(1.0);

        group.WithTags("TestApi");

        group.MapGet("/items", async (HttpContext _, ISender mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new TestQueryRequestRequest(), ct);

            return TypedResults.Ok(result.Value);
        });

        group.MapGet("/items/{id}", GetResourceById);


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
