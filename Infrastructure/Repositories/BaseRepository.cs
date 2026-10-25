using Application.Abstractions.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.MsSQL;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext Context;
    protected readonly DbSet<T> Set; 
    
    public BaseRepository(AppDbContext context)
    {
        Context = context;
        Set = context.Set<T>();
    }
    
    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct)
    { 
        return await Set.AsNoTracking().ToListAsync(ct);
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await Set.FindAsync([id], ct);
    }

    public void Add(T entity)
    {
        Set.Add(entity);
    }

    public void Update(T entity)
    {
        Set.Update(entity);
    }

    public void Remove(T entity)
    {
        Set.Remove(entity);
    }
}