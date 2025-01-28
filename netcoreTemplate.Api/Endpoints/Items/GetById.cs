using Application.Items.GetById;
using Application.Items.Service;
using FastEndpoints;

namespace Api.Endpoints.Items;

public class GetById(IItemService testService) : EndpointWithoutRequest<IReadOnlyCollection<TestQueryDto>>
{
    public override void Configure()
    {
        AllowAnonymous();
        Get("/test/{id}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<string>("id");

        var parsedId = Guid.Parse(id);
        var result = await testService.GetByIdAsync(parsedId, ct);

        await SendAsync(result.Value, cancellation: ct);
    }
}
