using Application.Test.GetById;
using Application.Test.Service;
using FastEndpoints;
using FluentResults;

namespace Api.Endpoints.Test.GetById;

public class GetByIdEndpoint(ITestService testService) : EndpointWithoutRequest<IResult<IReadOnlyCollection<TestQueryDto>>>
{
    public override void Configure()
    {
        AllowAnonymous();
        Get("/test/{id}");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<string>("id");

        var parsedId=Guid.Parse(id);
        await testService.GetById(parsedId);
        //return base.HandleAsync(req, ct);
    }
}
