using Library.Core.Entities;
using Library.Core.Specifications;

namespace Library.Core.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T?> GetEntityWithSpecificationAsync(ISpecification<T> spec);
    Task<IReadOnlyList<T?>> ListAsync(ISpecification<T> spec);
    Task<T> InsertAsync(T obj);
    Task<T> UpdateAsync(T obj);
    Task DeleteAsync(T obj);
}