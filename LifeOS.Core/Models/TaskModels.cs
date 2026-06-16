namespace LifeOS.Core.Models;

public class CreateTaskRequest
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public Priority Priority { get; set; } = Priority.Medium;
    public DateTime? DueDate { get; set; }
}

public class UpdateTaskRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public Priority? Priority { get; set; }
    public DateTime? DueDate { get; set; }
}