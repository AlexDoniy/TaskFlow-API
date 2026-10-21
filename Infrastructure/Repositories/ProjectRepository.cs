using Application.Abstractions.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.MsSQL;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProjectRepository : BaseRepository<ProjectEntity>, IProjectRepository
{
    public ProjectRepository(AppDbContext context) : base(context)
    {
        
    }
    
}