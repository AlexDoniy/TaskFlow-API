using Application.Abstractions.Interfaces.Repositories;
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
    
}