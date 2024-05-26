using MediatR;
using netcoreTemplate.Application.Features.Feature1.Dtos;

namespace netcoreTemplate.Application.Features.Feature1.CQRS.Querys;

public class TestQueryParamRequestRequest(Guid id) : IRequest<IReadOnlyCollection<TestQueryModel>>
{
    public Guid Id { get; private set; } = id;
}


public class TestQueryParamRequestHandler : IRequestHandler<TestQueryParamRequestRequest, IReadOnlyCollection<TestQueryModel>>
{
    public async Task<IReadOnlyCollection<TestQueryModel>> Handle(TestQueryParamRequestRequest request, CancellationToken cancellationToken)
    {
        TestQueryModel item = new(request.Id);
        return await Task.FromResult(new List<TestQueryModel> { item });
    }
}
