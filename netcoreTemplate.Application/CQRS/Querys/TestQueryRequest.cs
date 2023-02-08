using MediatR;

namespace netcoreTemplate.application.CQRS.Querys;

public class TestQueryRequestRequest : IRequest<IReadOnlyCollection<TestQueryModel>>
{
}

public class TestQueryRequestHandler : IRequestHandler<TestQueryRequestRequest, IReadOnlyCollection<TestQueryModel>>
{
    public Task<IReadOnlyCollection<TestQueryModel>> Handle(TestQueryRequestRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}