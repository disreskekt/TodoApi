using System.Linq.Expressions;
using Domain.Models;

namespace Api.Services.Interfaces;

public interface IValidationService<TTable>
    where TTable : BaseEntity
{
    public void ThrowIfFound<TColumn>(Expression<Func<TTable, TColumn>> columnExpression, TColumn valueToCheck);
    public void ThrowIfNotFound<TColumn>(Expression<Func<TTable, TColumn>> columnExpression, TColumn valueToCheck);
}