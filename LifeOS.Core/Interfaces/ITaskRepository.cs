using LifeOS.Core.Models;

namespace LifeOS.Core.Interfaces;

public interface ITaskRepository
{
    Task<List<TaskItem>> GetAllByUserIdAsync(Guid userId);
    Task<TaskItem?> GetByIdAsync(Guid userId);
    Task AddAsync(TaskItem taskItem);
    Task UpdateAsync(TaskItem taskItem);
    Task DeleteAsync(Guid id);
    Task SaveAsync();
}