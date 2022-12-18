using System.Linq.Expressions;
using Domain.Models;

namespace Domain.RepositoryInterfaces;

public interface IRepository<T>
    where T : BaseEntity
{
    public IEnumerable<T> GetAll();
    public IEnumerable<T> GetAll<TProp>(params Expression<Func<T, TProp>>[] includes);
    public Task<T?> Get(int id);
    public void Insert(T entity);
    public void Update(T entity);
    public void Delete(T entity);
    public Task SaveChangesAsync();
}