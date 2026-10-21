using Application.Abstractions.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.MsSQL;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TaskItemRepository : BaseRepository<TaskItemEntity>, ITaskItemRepository
{
    public TaskItemRepository(AppDbContext context) : base(context)
    {
        
    }
    
}