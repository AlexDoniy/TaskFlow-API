using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.Dtos.Tasks;

public record UpdateTaskItemRequest
{
    public required string Title { get; init; }
    
    public string? Description { get; init; }

    public TaskItemPriority Priority { get; init; }

    public DateTimeOffset? DueDate { get; init; }
}