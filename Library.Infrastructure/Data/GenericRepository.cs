using Library.Core.Entities;
using Library.Core.Interfaces;
using Library.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Data;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly LibraryDbContext _libraryDbContext;

    public GenericRepository(LibraryDbContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _libraryDbContext.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await _libraryDbContext.Set<T>().ToListAsync();
    }

    public async Task<T?> GetEntityWithSpecificationAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T?>> ListAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public async Task<T> InsertAsync(T obj)
    {
        _libraryDbContext.Set<T>().Add(obj);
        await _libraryDbContext.SaveChangesAsync();
        return obj;
    }

    public async Task<T> UpdateAsync(T obj)
    {
        _libraryDbContext.Set<T>().Update(obj);
        await _libraryDbContext.SaveChangesAsync();
        return obj;
    }

    public async Task DeleteAsync(T obj)
    {
        _libraryDbContext.Set<T>().Remove(obj);
        await _libraryDbContext.SaveChangesAsync();
    }

    private IQueryable<T?> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_libraryDbContext.Set<T>().AsQueryable(), spec);
    }
}