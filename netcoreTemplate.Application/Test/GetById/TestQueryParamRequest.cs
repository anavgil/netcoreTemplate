using FluentResults;
using MediatR;
using netcoreTemplate.Application.Test.Service;

namespace netcoreTemplate.Application.Test.GetById;

public class TestQueryParamRequestRequest(string id) : IRequest<IResult<IReadOnlyCollection<TestQueryDto>>>
{
    public string Id { get; private set; } = id;

    public Guid ParsedId => Guid.Parse(this.Id);
}


public class TestQueryParamRequestHandler(ITestService service) : IRequestHandler<TestQueryParamRequestRequest, IResult<IReadOnlyCollection<TestQueryDto>>>
{
    public async Task<IResult<IReadOnlyCollection<TestQueryDto>>> Handle(TestQueryParamRequestRequest request, CancellationToken cancellationToken)
    {
        return await service.GetById(request.ParsedId);
    }
}
