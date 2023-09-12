namespace Services.Dto.Requests;

public class RegisterRequest
{
    public string Name { get; init; }
    public string Login { get; init; }
    public string Password { get; init; }
}