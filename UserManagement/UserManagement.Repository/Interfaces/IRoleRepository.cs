using System.Linq.Expressions;
using UserManagement.Common.Models;

namespace UserManagement.Repository.Interfaces;

public interface IRoleRepository
{
    IQueryable<RoleModel> GetRoles(Expression<Func<RoleModel, bool>> filter = null);
}