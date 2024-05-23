using MediatR;
using System.Collections.ObjectModel;

namespace netcoreTemplate.Application.Features.Feature1.CQRS.Querys;

public class TestQueryRequestRequest : IRequest<IReadOnlyCollection<TestQueryModel>>
{
}

public class TestQueryRequestHandler : IRequestHandler<TestQueryRequestRequest, IReadOnlyCollection<TestQueryModel>>
{
    public async Task<IReadOnlyCollection<TestQueryModel>> Handle(TestQueryRequestRequest request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new Collection<TestQueryModel>());
    }
}