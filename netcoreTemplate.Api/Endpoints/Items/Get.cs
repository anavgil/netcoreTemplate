using Application.Items.Get;
using Application.Items.GetById;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Endpoints.Items;

public class Get(ISender sender) : EndpointWithoutRequest<Ok<IReadOnlyCollection<TestQueryDto>>>
{
    public override void Configure()
    {
        Get("/test");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await sender.Send(new TestQueryRequestRequest(), ct);

        await SendResultAsync(TypedResults.Ok(result.Value));
    }
}
