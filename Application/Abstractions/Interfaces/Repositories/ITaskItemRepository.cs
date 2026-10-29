using Application.Dtos;
using Domain.Entities;

namespace Application.Abstractions.Interfaces.Repositories;

public interface ITaskItemRepository : IBaseRepository<TaskItemEntity>
{
    Task<IReadOnlyList<TaskItemEntity>> GetAllByProjectAsync(Guid projectId, CancellationToken ct);
    Task<TaskItemEntity?> GetByIdInProjectAsync(Guid projectId, Guid id, CancellationToken ct);

    Task<PagedResult<TaskItemEntity>> GetPagedByProjectAsync(Guid projectId, int page, int pageSize,
        CancellationToken ct);
}