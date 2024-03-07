using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Contracts.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
}