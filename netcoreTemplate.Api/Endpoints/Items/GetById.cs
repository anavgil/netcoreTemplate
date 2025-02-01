using Application.Items.GetById;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Endpoints.Items;

public class GetById(ISender sender) : EndpointWithoutRequest<Results<Ok<IReadOnlyCollection<TestQueryDto>>, NotFound>>
{
    public override void Configure()
    {
        AllowAnonymous();
        Get("/test/{id}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<string>("id");

        var request = new TestQueryParamRequestRequest(id);
        var result = await sender.Send(request, ct);

        if (result.IsFailed)
        {
            await SendResultAsync(TypedResults.NotFound());
            return;
        }
        await SendResultAsync(TypedResults.Ok(result.Value));
    }
}
