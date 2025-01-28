using Application.Items.GetById;
using Application.Items.Service;
using FastEndpoints;
using FluentResults;

namespace Api.Endpoints.Items;

public class Get(IItemService testService) : EndpointWithoutRequest<IResult<IReadOnlyCollection<TestQueryDto>>>
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
        var r = await testService.GetAllAsync(ct);
        await SendAsync(r, cancellation: ct);
    }
}
