using Application.Authentication.Login;
using FluentResults;
using MediatR;

namespace Application.Users.Login;

public class LoginUserCommand(LoginRequestDto dto) : IRequest<IResult<LoginResponseDto>>
{
    public string UserName { get; private set; } = dto.Username;
    public string Password { get; private set; } = dto.Password;
}
