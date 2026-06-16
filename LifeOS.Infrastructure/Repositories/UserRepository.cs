using System.Text.Json;
using LifeOS.Core.Interfaces;
using LifeOS.Core.Models;
using LifeOS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LifeOS.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly LifeOSDbContext _context;

    public UserRepository(LifeOSDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> ExistsAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}