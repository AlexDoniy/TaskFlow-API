using Application.Abstractions;
using Application.Abstractions.Interfaces.Repositories;
using Application.Dtos;
using Domain.Entities;
using Infrastructure.Persistence.MsSQL;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TaskItemRepository : BaseRepository<TaskItemEntity>, ITaskItemRepository
{
    private readonly DbSet<TaskItemEntity> _taskItemSet; 
    
    public TaskItemRepository(AppDbContext context) : base(context)
    {
        _taskItemSet = Context.Set<TaskItemEntity>();
    }
    
    public async Task<IReadOnlyList<TaskItemEntity>> GetAllByProjectAsync(Guid projectId, CancellationToken ct)
    {
        return await _taskItemSet
            .AsNoTracking()
            .Where(t => t.ProjectId == projectId)
            .OrderByDescending(t =>  t.CreatedAt)
            .ToListAsync(ct);
    }

    public async Task<TaskItemEntity?> GetByIdInProjectAsync(Guid projectId, Guid id, CancellationToken ct)
    {
        return await _taskItemSet
            .Where(t => t.ProjectId == projectId)
            .FirstOrDefaultAsync(t => t.Id == id, ct);
    }

    public async Task<PagedResult<TaskItemEntity>> GetPagedByProjectAsync(Guid projectId, int page, int pageSize, CancellationToken ct)
    {
        var query = _taskItemSet
            .AsNoTracking()
            .Where(t => t.ProjectId == projectId);

        var totalCount = await query.CountAsync(ct);

        var items = await query
            .OrderByDescending(t => t.CreatedAt)
            .ThenBy(t => t.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PagedResult<TaskItemEntity>(items, totalCount);
    }
}