using LifeOS.Core.Models;

namespace LifeOS.Core.Interfaces;

public interface ITaskService
{
    Task<List<TaskItem>> GetTasksAsync(Guid userId);
    Task<TaskItem?> GetTaskByIdAsync(Guid id);
    Task<TaskItem> CreateTaskAsync(Guid userId, CreateTaskRequest request);
    Task<TaskItem> UpdateTaskAsync(Guid id, UpdateTaskRequest request);
    Task CompleteTaskAsync(Guid id);
    Task DeleteTaskAsync(Guid id);
}