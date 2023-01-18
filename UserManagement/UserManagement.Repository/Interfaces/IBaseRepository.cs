using System.Linq.Expressions;

namespace UserManagement.Repository.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> filter = null);
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter = null);
    TEntity? Get(Expression<Func<TEntity, bool>> filter = null);
    Task<bool> CreateAsync(TEntity model);
    Task<bool> UpdateAsync(TEntity model);
    bool Create(TEntity model);
    bool Update(TEntity model);
}