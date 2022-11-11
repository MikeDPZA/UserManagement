using System.Linq.Expressions;
using UserManagement.Common.Dto.User;
using UserManagement.Common.Generic;
using UserManagement.Common.Models;

namespace UserManagement.Services.Interfaces;

public interface IUserControllerService
{
    /// <summary>
    /// Get a paged list of users
    /// </summary>
    /// <param name="pageNum">The page number to return.</param>
    /// <param name="pageSize">The number of items to return per page.</param>
    PagedResponse<User> GetUsers(int pageNum, int pageSize);

    /// <summary>
    /// Get a paged list of users with a filter
    /// </summary>
    /// <param name="pageNum">The page number to return.</param>
    /// <param name="pageSize">The number of items to return per page.</param>
    /// <param name="filter">This is the filter expression that you want to apply to the query.</param>
    PagedResponse<User> GetUsers(int pageNum, int pageSize, UserFilterDto filter);

    /// <summary>
    /// Get a paged list of users with a filter and a column to order by
    /// </summary>
    /// <param name="pageNum">The page number to return.</param>
    /// <param name="pageSize">The number of items to return per page.</param>
    /// <param name="filter">This is the filter expression that will be used to filter the results.</param>
    /// <param name="orderBy">The property to order the results by.</param>
    /// <param name="ascending">Direction to order by</param>
    PagedResponse<User> GetUsers(int pageNum, int pageSize, UserFilterDto filter,
        Expression<Func<User, object>> orderBy, bool ascending = true);
}