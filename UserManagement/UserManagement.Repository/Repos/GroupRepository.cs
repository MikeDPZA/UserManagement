using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UserManagement.Common.Models;
using UserManagement.Repository.Context;
using UserManagement.Repository.Interfaces;

namespace UserManagement.Repository.Repos;

public class GroupRepository: BaseRepository<GroupModel>, IGroupRepository
{
    public GroupRepository(UserManagementContext ctx) : base(ctx)
    {
    }

    public IQueryable<GroupModel> GetGroups(Expression<Func<GroupModel, bool>> filter = null)
        => GetQueryable(filter);
}