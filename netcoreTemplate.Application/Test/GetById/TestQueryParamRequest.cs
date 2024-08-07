using MediatR;

namespace netcoreTemplate.Application.Test.GetById;

public class TestQueryParamRequestRequest(string id) : IRequest<IReadOnlyCollection<TestQueryDto>>
{
    public string Id { get; private set; } = id;
}


public class TestQueryParamRequestHandler : IRequestHandler<TestQueryParamRequestRequest, IReadOnlyCollection<TestQueryDto>>
{
    public async Task<IReadOnlyCollection<TestQueryDto>> Handle(TestQueryParamRequestRequest request, CancellationToken cancellationToken)
    {
        TestQueryDto item = new(Guid.Parse(request.Id));
        return await Task.FromResult(new List<TestQueryDto> { item });
    }
}
