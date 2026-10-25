using Domain.Enums;

namespace Domain.Entities;

public class TaskItemEntity : BaseEntity
{ 
    public required string Title { get; set; }
    public string? Description { get; set; }
    
    public TaskItemStatus Status { get; set; } = TaskItemStatus.Todo;
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;

    public DateTimeOffset? DueDate { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    
    public Guid ProjectId { get; set; }
    public ProjectEntity Project { get; set; } = null!;
}