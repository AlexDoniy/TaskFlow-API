using Application.Dtos.Projects;

namespace Application.Abstractions.Interfaces.Services;

public interface IProjectService
{
    Task<ProjectResponse> CreateAsync(CreateProjectRequest request, CancellationToken ct);
    Task<ProjectResponse> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<ProjectResponse>> GetAllAsync(CancellationToken ct);
    Task<ProjectResponse> UpdateAsync(Guid id, UpdateProjectRequest request, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);

}