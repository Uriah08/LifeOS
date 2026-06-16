namespace LifeOS.Core.Models;

public class RegisterRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
}

public class LoginRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class AuthResult
{
    public required string Username { get; set;}
    public required string Token { get; set; }
    public required string RefreshToken { get; set; }
    public required string Email { get; set; }
    public string Role { get; set; } = "user";
}