using Application.Dtos.Projects;

namespace Application.Abstractions.Interfaces.Services;

public interface IProjectService
{
    Task<ProjectResponse> CreateAsync(CreateProjectRequest request, CancellationToken ct);
    Task<ProjectResponse?> GetByIdAsync(int id, CancellationToken ct);
}