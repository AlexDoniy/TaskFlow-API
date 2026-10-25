using Application.Abstractions.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.MsSQL;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext Context;
    
    public BaseRepository(AppDbContext context)
    {
        Context = context;
    }
    
    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct)
    { 
        return await Context.Set<T>().AsNoTracking().ToListAsync(ct);
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await Context.Set<T>().FindAsync([id], ct);
    }

    public void Add(T entity)
    {
        Context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        Context.Set<T>().Update(entity);
    }

    public void Remove(T entity)
    {
        Context.Set<T>().Remove(entity);
    }
}