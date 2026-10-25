using Domain.Entities;

namespace Application.Abstractions.Interfaces.Repositories;

public interface IProjectRepository : IBaseRepository<ProjectEntity>
{
    public Task<bool> ExistsAsync(Guid projectId, CancellationToken ct);
}