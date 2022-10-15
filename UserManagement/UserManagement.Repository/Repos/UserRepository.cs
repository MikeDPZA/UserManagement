using System.Linq.Expressions;
using UserManagement.Common.Models;
using UserManagement.Repository.Context;
using UserManagement.Repository.Interfaces;

namespace UserManagement.Repository.Repos;

public class UserRepository: BaseRepository<UserManagementContext>, IUserRepository
{
    public UserRepository(UserManagementContext ctx) : base(ctx)
    {
    }

    public IQueryable<UserModel> GetUsers(Expression<Func<UserModel, bool>> filter)
        => Get<UserModel>(filter);
}