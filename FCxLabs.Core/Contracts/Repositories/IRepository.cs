using System.Linq.Expressions;

namespace FCxLabs.Core.Contracts.Repositories;

public interface IRepository<T> where T : class
{
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, 
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
        List<Expression<Func<T, object>>> includes = null,
        bool disableTracking = true);
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null);
    Task<T> GetByIdAsync(string id);
    Task<IReadOnlyList<T>> GetAllAsync();
}