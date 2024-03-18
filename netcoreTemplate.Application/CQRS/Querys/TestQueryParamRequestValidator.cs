using FluentValidation;
using netcoreTemplate.application.CQRS.Querys;

namespace netcoreTemplate.Application.CQRS.Querys
{
    public class TestQueryParamRequestValidator : AbstractValidator<TestQueryParamRequestRequest>
    {
        public TestQueryParamRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
