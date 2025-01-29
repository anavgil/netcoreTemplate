using Application.Items.Get;
using Application.Items.GetById;
using FastEndpoints;
using MediatR;

namespace Api.Endpoints.Items;

public class Get(ISender sender) : EndpointWithoutRequest<IReadOnlyCollection<TestQueryDto>>
{
    public override void Configure()
    {
        Get("/items");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await sender.Send(new TestQueryRequestRequest(), ct);

        await SendAsync(result.Value, cancellation: ct);
    }
}
