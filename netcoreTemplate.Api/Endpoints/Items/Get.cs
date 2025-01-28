using Application.Items.GetById;
using Application.Items.Service;
using FastEndpoints;
using FluentResults;

namespace Api.Endpoints.Items;

public class Get(IItemService testService) : EndpointWithoutRequest<IReadOnlyCollection<TestQueryDto>>
{
    public override void Configure()
    {
        Get("/test");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        //await SendAsync(new()
        //{
        //    FullName = $"{r.FirstName} {r.LastName}",
        //    Message = "Welcome to FastEndpoints..."
        //});
        var result = await testService.GetAllAsync(ct);
        await SendAsync(result.Value, cancellation: ct);
    }
}
