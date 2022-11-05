using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace UserManagement.Repository.Repos;

public abstract class BaseRepository<T> where T : DbContext
{
    protected readonly T Ctx;
    protected BaseRepository(T ctx)
    {
        Ctx = ctx;
    }

    /// <summary>
    /// FindMany returns an IQueryable{TEntity}, where TEntity is a class, and the IQueryable is the result of a Where
    /// clause on the Set of TEntity, where the Where clause is either the func parameter or a lambda that always returns
    /// true.
    /// </summary>
    /// <param name="func">The expression to use to filter the results.</param>
    protected IQueryable<TEntity> FindMany<TEntity>(Expression<Func<TEntity, bool>>? func) where TEntity: class
        => Ctx
            .Set<TEntity>()
            .Where(func ?? (_ => true));
    
    /// <summary>
    /// Finds the first entity that matches the given predicate
    /// </summary>
    /// <param name="func">The expression that will be used to find the entity.</param>
    protected TEntity? Find<TEntity>(Expression<Func<TEntity, bool>> func) where TEntity: class
        => Ctx
            .Set<TEntity>()
            .FirstOrDefault(func);
}