using System.Linq.Expressions;
using UserManagement.Common.Dto.Permission;
using UserManagement.Common.Generic;

namespace UserManagement.Services.Interfaces;

public interface IPermissionControllerService
{
    /// <summary>
    /// Get permissions
    /// </summary>
    /// <param name="pageNum">The page number to return</param>
    /// <param name="pageSize">The amount of records per page</param>
    /// <returns></returns>
    PagedResponse<Permission> GetPermissions(int pageNum, int pageSize);

    /// <summary>
    /// Get permissions
    /// </summary>
    /// <param name="pageNum">The page number to return</param>
    /// <param name="pageSize">The amount of records per page</param>
    /// <param name="filter">Filters to apply</param>
    /// <returns></returns>
    PagedResponse<Permission> GetPermissions(int pageNum, int pageSize, PermissionFilterDto? filter);

    /// <summary>
    /// Get permissions
    /// </summary>
    /// <param name="pageNum">The page number to return</param>
    /// <param name="pageSize">The amount of records per page</param>
    /// <param name="filter">Filters to apply</param>
    /// <param name="orderBy">Field to order by</param>
    /// <returns></returns>
    PagedResponse<Permission> GetPermissions(int pageNum, int pageSize, PermissionFilterDto? filter, Expression<Func<Permission, object>> orderBy);
}