using Application.Abstractions;
using FluentResults;
using MediatR;

namespace Application.Users.Login;

public class LoginUserCommandHandler(IAuthenticationService authenticationService) : IRequestHandler<LoginUserCommand, IResult<LoginResponseDto>>
{
    public async Task<IResult<LoginResponseDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        return await authenticationService.LoginAsync(request.UserName, request.Password);
    }
}
