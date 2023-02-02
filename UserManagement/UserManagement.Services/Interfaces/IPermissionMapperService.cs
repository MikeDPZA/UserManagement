using System.Linq.Expressions;
using UserManagement.Common.Dto.Permission;
using UserManagement.Common.Models;

namespace UserManagement.Services.Interfaces;

public interface IPermissionMapperService
{
    /// <summary>
    /// Build a filter expression for permissions
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Expression<Func<PermissionModel, bool>> MapToFilterExpression(PermissionFilterDto? filter);

    /// <summary>
    /// Map Permission Model to Permission
    /// </summary>
    /// <returns></returns>
    Expression<Func<PermissionModel, Permission>> MapToPermissionDto();
}