using Domain.Entities;

namespace Application.Abstractions.Interfaces.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct);
    Task<T?> GetByIdAsync(Guid id, CancellationToken ct);
    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);
}