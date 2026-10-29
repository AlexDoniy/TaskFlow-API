namespace Application.Dtos.Tasks;

public record GetTaskItemsRequest
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}

