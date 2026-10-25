using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.Dtos.Tasks;

public record UpdateTaskItemRequest
{
    [Required]
    [StringLength(200)]
    public required string Title { get; init; }

    [StringLength(2000)]
    public string? Description { get; init; }

    public TaskItemPriority Priority { get; init; }

    public TaskItemStatus Status{ get; init; }

    public DateTimeOffset? DueDate { get; init; }
}