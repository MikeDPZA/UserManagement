using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UserManagement.Common.Models;
using UserManagement.Repository.Context;
using UserManagement.Repository.Interfaces;

namespace UserManagement.Repository.Repos;

public class UserRepository: BaseRepository<UserModel>, IUserRepository
{
    public UserRepository(UserManagementContext ctx) : base(ctx) { }

    public IQueryable<UserModel> GetUsers(Expression<Func<UserModel, bool>>? filter)
        => GetQueryable(filter);

    public UserModel? GetUser(Expression<Func<UserModel, bool>> filter)
        => Get(filter);

    public Task<UserModel?> GetUserAsync(Expression<Func<UserModel, bool>> filter)
        => GetAsync(filter);
}