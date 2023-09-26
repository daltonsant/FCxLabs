using System.Linq.Expressions;
using FCxLabs.Core.Contracts.Repositories;
using FCxLabs.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace FCxLabs.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    
    protected readonly UserDbContext _db;
	
    public Repository(UserDbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }
	
    public async Task<T> CreateAsync(T entity)
    {
        await _db.Set<T>().AddAsync(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<T> DeleteAsync(T entity)
    {
        _db.Set<T>().Remove(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, 
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
        List<Expression<Func<T, object>>> includes = null, 
        bool disableTracking = true)
    {
        IQueryable<T> query = _db.Set<T>();
		
        if(disableTracking)
            query = query.AsNoTracking();
        if(includes is not null)
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        if(predicate is not null)
            query = query.Where(predicate);
        if(orderBy is not null)
            return await orderBy(query).ToListAsync();
		
        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null)
    {
        return await GetAsync(predicate,null);
    }

    public async Task<T> GetByIdAsync(string id)
    {
        return await _db.Set<T>().FindAsync(id);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _db.Set<T>().Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    async Task<IReadOnlyList<T>> IRepository<T>.GetAllAsync()
    {
        return await _db.Set<T>().ToListAsync();
    }
}