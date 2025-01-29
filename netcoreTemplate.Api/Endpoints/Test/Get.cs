using Application.Test.Get;
using Application.Test.GetById;
using FastEndpoints;
using MediatR;

namespace Api.Endpoints.Test;

public class Get(ISender sender) : EndpointWithoutRequest<IReadOnlyCollection<TestQueryDto>>
{
    public override void Configure()
    {
        Get("/test");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await sender.Send(new TestQueryRequestRequest(), ct);

        await SendAsync(result.Value, cancellation: ct);
    }
}
