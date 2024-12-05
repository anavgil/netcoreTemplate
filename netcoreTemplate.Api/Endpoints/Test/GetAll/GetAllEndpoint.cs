using Application.Test.GetById;
using Application.Test.Service;
using FastEndpoints;
using FluentResults;

namespace Api.Endpoints.Test.GetAll;

public class GetAllEndpoint(ITestService testService) : EndpointWithoutRequest<IResult<IReadOnlyCollection<TestQueryDto>>>
{
    public override void Configure()
    {
        Get("/test");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await testService.GetAll();
    }
}
