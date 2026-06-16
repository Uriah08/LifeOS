using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LifeOS.Core.Interfaces;
using LifeOS.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LifeOS.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _config;

    public AuthService(IUserRepository userRepository, IConfiguration config)
    {
        _userRepository = userRepository;
        _config = config;
    }

    public async Task<AuthResult> RegisterAsync(RegisterRequest request)
    {
        var exists = await _userRepository.ExistsAsync(request.Email);
        if (exists) throw new Exception("User already exists.");

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = "user"
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveAsync();

        return GenerateToken(user);
    }

    private AuthResult GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.Name, user.Username)
        };

        var token = new JwtSecurityToken(
            issuer: _config["JwtSettings:Issuer"],
            audience: _config["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                double.Parse(_config["JwtSettings:ExpiryInMinutes"]!)),
            signingCredentials: credentials
        );
        
        return new AuthResult
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = string.Empty,
            Username = user.Username,
            Role = user.Role,
            Email = user.Email
        };
    }
}