using Application.Items.GetById;
using Application.Items.Service;
using FastEndpoints;
using FluentResults;

namespace Api.Endpoints.Items;

public class GetById(IItemService testService) : EndpointWithoutRequest<IResult<IReadOnlyCollection<TestQueryDto>>>
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

        await SendAsync(result, cancellation: ct);
        //return base.HandleAsync(req, ct);
    }
}
