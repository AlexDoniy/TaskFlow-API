using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.Dtos.Tasks;

public record CreateTaskItemRequest
{
    [Required]
    [StringLength(200)]
    public required string Title { get; init; }

    [StringLength(2000)]
    public string? Description { get; init; }

    public TaskItemPriority Priority { get; init; } = TaskItemPriority.Medium;
    
    public DateTimeOffset? DueDate { get; init; }
}