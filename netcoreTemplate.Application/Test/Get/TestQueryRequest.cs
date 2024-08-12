using Application.Test.GetById;
using Application.Test.Service;
using FluentResults;
using MediatR;

namespace Application.Test.Get;

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