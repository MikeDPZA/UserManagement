using System.Linq.Expressions;
using UserManagement.Common.Models;
using UserManagement.Repository.Context;
using UserManagement.Repository.Interfaces;

namespace UserManagement.Repository.Repos;

public class UserRepository: BaseRepository<UserManagementContext>, IUserRepository
{
    public UserRepository(UserManagementContext ctx) : base(ctx) { }

    public IQueryable<UserModel> GetUsers(Expression<Func<UserModel, bool>>? filter)
        => FindMany(filter);

    public UserModel? GetUser(Expression<Func<UserModel, bool>> filter)
        => Find(filter);
}