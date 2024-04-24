using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Repositories;

public class GenericRepository<T>(CaDatabaseContext context) : IGenericRepository<T>
    where T : BaseEntity
{
    protected readonly CaDatabaseContext Context = context;

    public async Task<IReadOnlyList<T>> GetAsync() => await Context.Set<T>().AsNoTracking().ToListAsync();

    public async Task<T> GetByIdAsync(int id) => await Context.Set<T>()
        .AsNoTracking()
        .FirstOrDefaultAsync(q => q.Id == id);

    public async Task CreateAsync(T entity)
    {
        await Context.AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        Context.Remove(entity);
        await Context.SaveChangesAsync();
    }
    
    protected async Task SaveChangesAsync()
    {
        await Context.SaveChangesAsync();
    }
}