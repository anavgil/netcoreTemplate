using MediatR;
using netcoreTemplate.Application.Test.Service;

namespace netcoreTemplate.Application.Test.GetById;

public class TestQueryParamRequestRequest(string id) : IRequest<IReadOnlyCollection<TestQueryDto>>
{
    public string Id { get; private set; } = id;

    public Guid ParsedId => Guid.Parse(this.Id);
}


public class TestQueryParamRequestHandler(ITestService service) : IRequestHandler<TestQueryParamRequestRequest, IReadOnlyCollection<TestQueryDto>>
{
    public async Task<IReadOnlyCollection<TestQueryDto>> Handle(TestQueryParamRequestRequest request, CancellationToken cancellationToken)
    {
        var result = await service.GetById(request.ParsedId);

        return result.Value;
    }
}
