using System.Linq.Expressions;
using UserManagement.Common.Dto;
using UserManagement.Common.Models;

namespace UserManagement.Repository.Interfaces;

public interface IPermissionRepository
{
    UserPermissionDetailsDto GetUserPermissionDetails(Guid userId);
    IQueryable<PermissionModel> GetPermissions(Expression<Func<PermissionModel, bool>> filter = null);
}