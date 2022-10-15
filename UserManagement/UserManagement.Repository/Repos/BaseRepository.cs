using System.Diagnostics.CodeAnalysis;
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

    protected IQueryable<TEntity> Get<TEntity>([NotNull]Expression<Func<TEntity, bool>> func) where TEntity: class
        => Ctx.Set<TEntity>().Where(func);
}