using System.Linq.Expressions;
using UserManagement.Common.Models;

namespace UserManagement.Repository.Interfaces;

public interface IGroupRepository
{
    IQueryable<GroupModel> GetGroups(Expression<Func<GroupModel, bool>> filter = null);
}