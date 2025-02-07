using FluentValidation;

namespace Application.Users.Register;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Username is required");

        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .WithMessage("Email is required");

        RuleFor(x => x.Pass)
            .NotEmpty()
            .NotNull()
            .WithMessage("Password is required");

        //RuleFor(x => x.Pass)
        //    .MinimumLength(6)
        //    .WithMessage("Password must be at least 6 characters long");

        RuleFor(x => x.User)
            .NotEmpty()
            .NotNull()
            .WithMessage("User is required");

        RuleFor(x => x.Surname)
            .NotEmpty()
            .NotNull()
            .WithMessage("Surname is required");
    }
}
