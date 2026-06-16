using LifeOS.Core.Models;

namespace LifeOS.Core.Interfaces;

public interface IAuthService
{
    Task<AuthResult> LoginAsync(LoginRequest request);
    Task<AuthResult> RegisterAsync(RegisterRequest request);
}