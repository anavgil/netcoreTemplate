
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using netcoreTemplate.Application.Test.Get;
using netcoreTemplate.Application.Test.GetById;

namespace netcoreTemplate.Api.Modules;

public class EndpointTest : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("testApi");

        group.WithTags("TestApi");

        group.MapGet("/", async (HttpContext _, ISender mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new TestQueryRequestRequest(), ct);

            return TypedResults.Ok(result);
        });

        group.MapGet("/{id}", GetResourceById);


        static async Task<Results<Ok<IReadOnlyCollection<TestQueryDto>>, ValidationProblem>> GetResourceById(HttpContext _, string id, ISender mediator, CancellationToken ct)
        {
            var validator = new TestQueryParamRequestValidator();
            var request = new TestQueryParamRequestRequest(id);

            var validationResult = validator.Validate(request);

            if (validationResult.IsValid)
            {
                var result = await mediator.Send(request, ct);

                return TypedResults.Ok(result);
            }
            else
            {
                return TypedResults.ValidationProblem(validationResult.ToDictionary());
            }
        }
    }
}
