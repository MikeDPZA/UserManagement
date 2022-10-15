using System.Linq.Expressions;
using UserManagement.Common.Models;

namespace UserManagement.Repository.Interfaces;

public interface IUserRepository
{
    IQueryable<UserModel> GetUsers(Expression<Func<UserModel, bool>> filter);
}