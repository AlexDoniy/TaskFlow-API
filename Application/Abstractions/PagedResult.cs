namespace Application.Abstractions;

public record PagedResult<T>(IReadOnlyList<T> Items, int TotalCount);