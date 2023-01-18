using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UserManagement.Repository.Context;
using UserManagement.Repository.Interfaces;

namespace UserManagement.Repository.Repos;

public abstract class BaseRepository<TEntity>: IBaseRepository<TEntity> where TEntity : class 
{
    protected readonly UserManagementContext Ctx;
    protected BaseRepository(UserManagementContext ctx)
    {
        Ctx = ctx;
    }

    /// <summary>
    /// FindMany returns an IQueryable{TEntity}, where TEntity is a class, and the IQueryable is the result of a Where
    /// clause on the Set of TEntity, where the Where clause is either the func parameter or a lambda that always returns
    /// true.
    /// </summary>
    /// <param name="func">The expression to use to filter the results.</param>
    public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>>? func)
        => Ctx
            .Set<TEntity>()
            .Where(func ?? (_ => true));

    /// <summary>
    /// Finds the first entity that matches the given predicate
    /// </summary>
    /// <param name="func">The expression that will be used to find the entity.</param>
    public TEntity? Get(Expression<Func<TEntity, bool>> func)
        => Ctx
            .Set<TEntity>()
            .FirstOrDefault(func  ?? (_ => true));
    
    /// <summary>
    /// Finds the first entity that matches the given predicate
    /// </summary>
    /// <param name="func">The expression that will be used to find the entity.</param>
    /// <returns></returns>
    public Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> func)
        => Ctx
            .Set<TEntity>()
            .FirstOrDefaultAsync(func  ?? (_ => true));

    public async Task<bool> CreateAsync(TEntity model)
    {
        Ctx.Set<TEntity>().Add(model);
        return await SaveChangesAsync();
    }
    
    public async Task<bool> UpdateAsync(TEntity model)
    {
        Ctx.Set<TEntity>().Add(model);
        return await SaveChangesAsync();
    }
    
    public bool Create(TEntity model)
    {
        Ctx.Set<TEntity>().Add(model);
        return SaveChanges();
    }
    
    public bool Update(TEntity model)
    {
        Ctx.Set<TEntity>().Add(model);
        return SaveChanges();
    }
    
    protected async Task<bool> SaveChangesAsync()
    {
        try
        {
            return await Ctx.SaveChangesAsync() > 0;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    
    protected bool SaveChanges()
    {
        try
        {
            return Ctx.SaveChanges() > 0;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}