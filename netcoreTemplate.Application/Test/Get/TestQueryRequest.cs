using MediatR;
using netcoreTemplate.Application.Test.GetById;
using netcoreTemplate.Application.Test.Service;

namespace netcoreTemplate.Application.Test.Get;

public class TestQueryRequestRequest : IRequest<IReadOnlyCollection<TestQueryDto>>
{
}

public class TestQueryRequestHandler(ITestService service) : IRequestHandler<TestQueryRequestRequest, IReadOnlyCollection<TestQueryDto>>
{
    public async Task<IReadOnlyCollection<TestQueryDto>> Handle(TestQueryRequestRequest request, CancellationToken cancellationToken)
    {
        var result = await service.GetAll();

        return result.Value;
    }
}