namespace Application.Users.Dtos
{
    public record RegisterRequestDto(string Name = "", string Surname = "", string User = "", string Pass = "", string Email = "");
}
