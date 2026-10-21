using Application.Abstractions;
using Application.Abstractions.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.MsSQL;

public class AppDbContext : DbContext, IUnitOfWork
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