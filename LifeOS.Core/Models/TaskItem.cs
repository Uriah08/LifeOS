namespace LifeOS.Core.Models;

public class TaskItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; } = false;
    public Priority Priority { get; set; } = Priority.Medium;
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set;}
    public bool IsArchived { get; set; } = false;
}

public enum Priority
{
    Low,
    Medium,
    High
}