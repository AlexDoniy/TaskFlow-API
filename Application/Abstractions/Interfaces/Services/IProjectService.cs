using Application.Dtos.Projects;

namespace Application.Abstractions.Interfaces.Services;

public interface IProjectService
{
    Task<ProjectResponse> CreateAsync(CreateProjectRequest request, CancellationToken ct);
    Task<ProjectResponse?> GetByIdAsync(int id, CancellationToken ct);
    Task<IReadOnlyList<ProjectResponse>> GetAllAsync(CancellationToken ct);
    Task<ProjectResponse?> UpdateAsync(int id, UpdateProjectRequest request, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);

}