using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UserManagement.Common.Models;
using UserManagement.Repository.Context;
using UserManagement.Repository.Interfaces;

namespace UserManagement.Repository.Repos;

public class RoleRepository: BaseRepository<RoleModel>, IRoleRepository
{
    public RoleRepository(UserManagementContext ctx) : base(ctx)
    {
    }

    public IQueryable<RoleModel> GetRoles(Expression<Func<RoleModel, bool>> filter = null)
        => GetQueryable(filter);
}