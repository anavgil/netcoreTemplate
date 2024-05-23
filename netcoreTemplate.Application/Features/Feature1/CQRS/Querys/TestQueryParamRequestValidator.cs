using FluentValidation;

namespace netcoreTemplate.Application.Features.Feature1.CQRS.Querys;

public class TestQueryParamRequestValidator : AbstractValidator<TestQueryParamRequestRequest>
{
    public TestQueryParamRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
