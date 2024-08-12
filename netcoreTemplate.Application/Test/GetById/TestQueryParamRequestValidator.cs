using FluentValidation;

namespace Application.Test.GetById;

public class TestQueryParamRequestValidator : AbstractValidator<TestQueryParamRequestRequest>
{
    public TestQueryParamRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .Must(p => Guid.TryParse(p, out _)).WithMessage("El valor debe ser un GUID válido.");
    }
}
