using System.Linq.Expressions;
using Domain.Models;

namespace Domain.RepositoryInterfaces;

public interface IRepository<T>
    where T : BaseEntity
{
    public IQueryable<T> GetAll();
    public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
    public IQueryable<T> GetAll<TProp>(Expression<Func<T, TProp>> include);
    public Task<T?> Get(int id);
    public Task<T?> GetInclude<TProp>(int id, Expression<Func<T, TProp>> include);
    public void Insert(T entity);
    public void Update(T entity);
    public void Delete(T entity);
    public Task SaveChangesAsync();
}