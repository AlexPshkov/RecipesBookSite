namespace Services.Dto.Requests;

public class ChangeUserDataRequest : UserDto
{
    public string Id { get; init; }
    public string Password { get; init; }
}