namespace Domain.Entities;

public class ProjectEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }

    public List<TaskItemEntity> Tasks { get; set; } = [];
}