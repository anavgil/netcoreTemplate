using Application.Users.Dtos;
using FluentResults;
using MediatR;

namespace Application.Users.Register;

public class RegisterUserCommand(RegisterRequestDto dto) : IRequest<IResult<string>>
{
    public string Name { get; private set; } = dto.Name;
    public string Surname { get; private set; } = dto.Surname;
    public string User { get; private set; } = dto.User;
    public string Pass { get; private set; } = dto.Pass;
    public string Email { get; private set; } = dto.Email;
}
