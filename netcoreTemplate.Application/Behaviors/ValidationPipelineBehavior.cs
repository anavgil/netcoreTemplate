using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application.Behaviors;

internal sealed class ValidationPipelineBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(next);

        if (validators.Any())
        {
            ValidationFailure[] validationFailures = await ValidateAsync(request, cancellationToken);

            if(validationFailures.Length != 0)
            {
                throw new ValidationException(validationFailures);
            }
        }
        return await next().ConfigureAwait(false);
    }

    private async Task<ValidationFailure[]> ValidateAsync(TRequest request, CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return [];
        }

        var context = new ValidationContext<TRequest>(request);

        ValidationResult[] validationResults = await Task.WhenAll(
                            validators.Select(validator => validator.ValidateAsync(context, cancellationToken))).ConfigureAwait(false);

        ValidationFailure[] validationFailures = validationResults
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .ToArray();

        return validationFailures;
    }

    //private static ValidationError CreateValidationError(ValidationFailure[] validationFailures) =>
    //    new(validationFailures.Select(f => Error.Problem(f.ErrorCode, f.ErrorMessage)).ToArray());
}
