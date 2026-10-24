

namespace Application.Dtos.Projects;

public record ProjectResponse
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string? Description { get; init; }
    
    public required DateTimeOffset CreatedAt { get; init; }

}