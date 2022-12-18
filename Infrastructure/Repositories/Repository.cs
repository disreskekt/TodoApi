using System.Linq.Expressions;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class Repository<T> : IRepository<T>
    where T : BaseEntity
{
    private readonly DataContext _db;
    private readonly DbSet<T> _dbSet;

    public Repository(DataContext db)
    {
        _db = db;
        _dbSet = db.Set<T>();
    }
    
    public IEnumerable<T> GetAll()
    {
        return _dbSet;
    }
    
    public IEnumerable<T> GetAll<TProp>(Expression<Func<T, TProp>>[] includes)
    {
        IEnumerable<T> includableQueryable = _dbSet;
        
        foreach (Expression<Func<T,TProp>> include in includes)
        {
            includableQueryable = _dbSet.Include(include);
        }
        
        return includableQueryable;
    }
    
    public async Task<T?> Get(int id)
    {
        return await _dbSet.FindAsync(id);
    }
    
    public void Insert(T entity)
    {
        _dbSet.Add(entity);
    }
    
    public void Update(T entity)
    {
        throw new NotImplementedException();
    }
    
    public void Delete(T entity)
    {
        throw new NotImplementedException();
        
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}