using FluentValidation;
using netcoreTemplate.Application.Features.Feature1.CQRS.Querys;

namespace netcoreTemplate.Application.Features.Feature1.Validators;

public class TestQueryParamRequestValidator : AbstractValidator<TestQueryParamRequestRequest>
{
    public TestQueryParamRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
