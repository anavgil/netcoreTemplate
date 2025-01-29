using Application.Items.GetById;
using FastEndpoints;
using MediatR;

namespace Api.Endpoints.Items;

public class GetById(ISender sender) : EndpointWithoutRequest<IReadOnlyCollection<TestQueryDto>>
{
    public override void Configure()
    {
        AllowAnonymous();
        Get("/items/{id}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<string>("id");

        var request = new TestQueryParamRequestRequest(id);
        var result = await sender.Send(request, ct);

        await SendAsync(result.Value, cancellation: ct);
    }
}
