using System.Linq.Expressions;
using UserManagement.Common.Models;

namespace UserManagement.Repository.Interfaces;

public interface IUserRepository
{
    /// <summary>
    /// Returns a users based on a query
    /// </summary>
    /// <param name="filter">An expression that filters the users to return.</param>
    IQueryable<UserModel> GetUsers(Expression<Func<UserModel, bool>>? filter = null);
    
    /// <summary>
    /// Find a user by a filter
    /// </summary>
    /// <param name="filter">Filter to apply to the users set</param>
    /// <returns></returns>
    UserModel? GetUser(Expression<Func<UserModel, bool>> filter = null);
    
    /// <summary>
    /// Find a user by a filter
    /// </summary>
    /// <param name="filter">Filter to apply to the users set</param>
    /// <returns></returns>
    Task<UserModel?> GetUserAsync(Expression<Func<UserModel, bool>> filter = null);
}