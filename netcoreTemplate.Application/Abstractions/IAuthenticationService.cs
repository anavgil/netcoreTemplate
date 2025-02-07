using Application.Users.Login;
using FluentResults;

namespace Application.Abstractions;

public interface IAuthenticationService
{
    Task<IResult<LoginResponseDto>> LoginAsync(string user, string pass);

    Task<IResult<LoginResponseDto>> RefreshTokenAsync(LoginResponseDto loginResponseDto);
    Task<IResult<string>> CreateUserAsync(string name, string surname, string user, string pass, string email);
}
