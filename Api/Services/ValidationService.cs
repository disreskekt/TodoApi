using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Api.Services.Interfaces;
using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Api.Services;

public class ValidationService<TTable> : IValidationService<TTable>
    where TTable : BaseEntity
{
    private readonly IRepository<TTable> _repository;

    public ValidationService(IRepository<TTable> repository)
    {
        _repository = repository;
    }

    public void ThrowIfFound<TColumn>(Expression<Func<TTable, TColumn>> columnExpression, TColumn valueToCheck)
    {
        bool contains = CheckIfDbContains(columnExpression, valueToCheck);

        if (contains)
        {
            throw new ValidationException();
        }
    }
    
    public void ThrowIfNotFound<TColumn>(Expression<Func<TTable, TColumn>> columnExpression, TColumn valueToCheck)
    {
        bool contains = CheckIfDbContains(columnExpression, valueToCheck);

        if (!contains)
        {
            throw new ValidationException();
        }
    }
    
    private bool CheckIfDbContains<TColumn>(Expression<Func<TTable, TColumn>> columnExpression, TColumn valueToCheck)
    {
        if (columnExpression.Body is not MemberExpression memberExpression)
        {
            throw new Exception();
        }
        
        Type entityType = typeof(TTable);
        string entityName = nameof(TTable);
        string propertyName = memberExpression.Member.Name;
        
        ParameterExpression parameter = Expression.Parameter(entityType, entityName);
        
        Expression<Func<TTable, bool>> lambda = Expression.Lambda<Func<TTable, bool>>(
            Expression.Equal(
                Expression.Property(parameter, propertyName),
                Expression.Constant(valueToCheck)
            ),
            parameter);
        
        return _repository.Any(lambda);
    }
}