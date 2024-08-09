using FluentResults;
using MediatR;
using netcoreTemplate.Application.Test.GetById;
using netcoreTemplate.Application.Test.Service;

namespace netcoreTemplate.Application.Test.Get;

public class TestQueryRequestRequest : IRequest<IResult<IReadOnlyCollection<TestQueryDto>>>
{
}

public class TestQueryRequestHandler(ITestService service) : IRequestHandler<TestQueryRequestRequest, IResult<IReadOnlyCollection<TestQueryDto>>>
{
    public async Task<IResult<IReadOnlyCollection<TestQueryDto>>> Handle(TestQueryRequestRequest request, CancellationToken cancellationToken)
    {
        return await service.GetAll();
    }
}