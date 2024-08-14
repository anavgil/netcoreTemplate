using Application.Test.Get;
using Application.Test.GetById;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Endpoints;

public class EndpointTest : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("testApi");

        group.WithTags("TestApi");

        group.MapGet("/", async (HttpContext _, ISender mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new TestQueryRequestRequest(), ct);

            return TypedResults.Ok(result.Value);
        });

        group.MapGet("/{id}", GetResourceById);


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
