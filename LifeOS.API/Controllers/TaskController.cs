using LifeOS.Core.Interfaces;
using LifeOS.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace LifeOS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetTasks(Guid userId)
    {
        var tasks = await _taskService.GetTasksAsync(userId);
        return Ok(tasks);
    }

    [HttpPost("{userId}")]
    public async Task<IActionResult> CreateTask(Guid userId, CreateTaskRequest request)
    {
        try
        {
            var task = await _taskService.CreateTaskAsync(userId, request);
            return Ok(task);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(Guid id, UpdateTaskRequest request)
    {
        try
        {
            var task = await _taskService.UpdateTaskAsync(id, request);
            return Ok(task);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}/complete")]
    public async Task<IActionResult> CompleteTask(Guid id)
    {
        try
        {
            await _taskService.CompleteTaskAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        try
        {
            await _taskService.DeleteTaskAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}