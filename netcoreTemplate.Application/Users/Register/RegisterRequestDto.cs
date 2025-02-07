namespace Application.Users.Register
{
    public record RegisterRequestDto(string Name = "", string Surname = "", string User = "", string Pass = "", string Email = "");
}
