
using Domain.Enums;

namespace Application.Dtos.Tasks;

public record TaskItemResponse
{
    public Guid Id { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public TaskItemStatus? Status { get; init; }
    public TaskItemPriority? Priority { get; init; }
    public Guid ProjectId { get; init; }
    public DateTimeOffset? DueDate { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}