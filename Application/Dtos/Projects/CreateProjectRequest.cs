using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Projects;

public record CreateProjectRequest
{
    [Required]
    [StringLength(200)]
    public required string Name { get; init; }

    [StringLength(2000)]
    public string? Description { get; init; }
}