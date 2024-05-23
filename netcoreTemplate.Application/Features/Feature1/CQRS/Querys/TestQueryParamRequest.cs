using MediatR;

namespace netcoreTemplate.Application.Features.Feature1.CQRS.Querys;

public class TestQueryParamRequestRequest : IRequest<IReadOnlyCollection<TestQueryModel>>
{
    public Guid Id { get; private set; }

    public TestQueryParamRequestRequest(Guid id) => Id = id;
}


public class TestQueryParamRequestHandler : IRequestHandler<TestQueryParamRequestRequest, IReadOnlyCollection<TestQueryModel>>
{
    public async Task<IReadOnlyCollection<TestQueryModel>> Handle(TestQueryParamRequestRequest request, CancellationToken cancellationToken)
    {
        TestQueryModel item = new() { Id = request.Id };
        return await Task.FromResult(new List<TestQueryModel> { item });
    }
}
