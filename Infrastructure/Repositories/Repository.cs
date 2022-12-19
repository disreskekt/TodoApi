using System.Linq.Expressions;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

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
    
    public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.Where(predicate);
    }
    
    public IQueryable<T> GetAll<TProp>(Expression<Func<T, TProp>> include, params Expression<Func<T, bool>>[] predicates)
    {
        IIncludableQueryable<T,TProp> included = _dbSet.Include(include);
        
        if (predicates.Length < 1)
        {
            return included;
        }

        IQueryable<T>? predicated = null;
        foreach (Expression<Func<T,bool>> predicate in predicates)
        {
            predicated = included.Where(predicate);
        }
        
        return predicated!;
    }
    
    public async Task<T?> Get(int id)
    {
        return await _dbSet.FindAsync(id);
    }
    
    public async Task<T?> GetInclude<TProp>(int id, Expression<Func<T, TProp>> include)
    {
        return await _dbSet.Include(include).FirstOrDefaultAsync(entity => entity.Id == id);
    }
    
    public void Insert(T entity)
    {
        _dbSet.Add(entity);
    }
    
    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }
    
    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}