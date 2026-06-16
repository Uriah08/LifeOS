using LifeOS.Core.Interfaces;
using LifeOS.Core.Models;
using LifeOS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LifeOS.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly LifeOSDbContext _context;
    public TaskRepository(LifeOSDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskItem>> GetAllByUserIdAsync(Guid userId)
    {
        return await _context.Tasks
        .Where(t => t.UserId == userId && !t.IsArchived)
        .OrderBy(t => t.DueDate)
        .ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id)
    {
        return await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id && !t.IsArchived);
    }

    public async Task AddAsync(TaskItem task)
    {
        await _context.Tasks.AddAsync(task);
    }

    public async Task UpdateAsync(TaskItem task)
    {
        task.UpdatedAt = DateTime.UtcNow;
        _context.Tasks.Update(task);
    }

    public async Task DeleteAsync(Guid id)
    {
        var task = await GetByIdAsync(id);
        if (task is null) return;

        task.IsArchived = true;
        await UpdateAsync(task);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}