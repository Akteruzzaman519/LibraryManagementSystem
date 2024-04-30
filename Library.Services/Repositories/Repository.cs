using Library.Infrastructure.LibraryDBContext;
using Library.Services.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Library.Services.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet;
    private readonly LibraryDbContext _libraryDbContext;

    public Repository(LibraryDbContext libraryDbContext)
    {
        _dbSet = libraryDbContext.Set<T>();
        _libraryDbContext = libraryDbContext;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
       return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _libraryDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Attach(entity);
        _libraryDbContext.Entry(entity).State = EntityState.Modified;
        await _libraryDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
         _dbSet.Remove(entity);
        await _libraryDbContext.SaveChangesAsync();

    }
}
