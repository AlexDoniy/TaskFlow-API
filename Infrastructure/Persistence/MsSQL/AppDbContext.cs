using Domain.Entities;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrasructure.Persistence.MsSQL;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) :
        base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new TaskItemConfiguration());
    }

    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<TaskItemEntity> Tasks { get; set; }
    
    
}