using MediatR;
using netcoreTemplate.Application.Test.GetById;
using System.Collections.ObjectModel;

namespace netcoreTemplate.Application.Test.Get;

public class TestQueryRequestRequest : IRequest<IReadOnlyCollection<TestQueryDto>>
{
}

public class TestQueryRequestHandler : IRequestHandler<TestQueryRequestRequest, IReadOnlyCollection<TestQueryDto>>
{
    public async Task<IReadOnlyCollection<TestQueryDto>> Handle(TestQueryRequestRequest request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new Collection<TestQueryDto>());
    }
}