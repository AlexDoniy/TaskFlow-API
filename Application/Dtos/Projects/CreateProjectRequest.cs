using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Projects;

public record CreateProjectRequest
{

    public required string Name { get; init; }

    public string? Description { get; init; }
}