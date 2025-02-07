using Application.Abstractions;
using FluentResults;
using MediatR;

namespace Application.Users.Register;

public class RegisterUserCommandHandler(IAuthenticationService authenticationService) : IRequestHandler<RegisterUserCommand, IResult<string>>
{
    public async Task<IResult<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        return await authenticationService.CreateUserAsync(request.Name, request.Surname, request.User, request.Pass, request.Email);
    }
}
