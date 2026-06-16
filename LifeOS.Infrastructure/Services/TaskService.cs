using LifeOS.Core.Interfaces;
using LifeOS.Core.Models;

namespace LifeOS.Infrastructure.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<List<TaskItem>> GetTasksAsync(Guid userId)
    {
        return await _taskRepository.GetAllByUserIdAsync(userId);
    }

    public async Task<TaskItem?> GetTaskByIdAsync(Guid id)
    {
        return await _taskRepository.GetByIdAsync(id);
    }

    public async Task<TaskItem> CreateTaskAsync(Guid userId, CreateTaskRequest request)
    {
        var task = new TaskItem
        {
            UserId = userId,
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority,
            DueDate = request.DueDate
        };

        await _taskRepository.AddAsync(task);
        await _taskRepository.SaveAsync();

        return task;
    }

    public async Task<TaskItem> UpdateTaskAsync(Guid id, UpdateTaskRequest request)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task is null) throw new Exception("Task not found.");

        if (request.Title is not null) task.Title = request.Title;
        if (request.Description is not null) task.Description = request.Description;
        if (request.Priority is not null) task.Priority = request.Priority.Value;
        if (request.DueDate is not null) task.DueDate = request.DueDate;

        await _taskRepository.UpdateAsync(task);
        await _taskRepository.SaveAsync();

        return task;
    }

    public async Task CompleteTaskAsync(Guid id)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task is null) throw new Exception("Task not found.");

        task.IsCompleted = true;

        await _taskRepository.UpdateAsync(task);
        await _taskRepository.SaveAsync();
    }

    public async Task DeleteTaskAsync(Guid id)
    {
        await _taskRepository.DeleteAsync(id);
        await _taskRepository.SaveAsync();
    }
}