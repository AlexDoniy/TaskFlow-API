using Application.Abstractions.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.MsSQL;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProjectRepository : BaseRepository<ProjectEntity>, IProjectRepository
{
    private readonly DbSet<ProjectEntity> _projectSet;

    public ProjectRepository(AppDbContext context) : base(context)
    {
        _projectSet = Context.Set<ProjectEntity>();
    }
    
    
    public async Task<bool> ExistsAsync(Guid projectId, CancellationToken ct)
    {
        var result = await _projectSet
            .FirstOrDefaultAsync(p => p.Id == projectId, ct);
        return result is not null;
    }
    
}

