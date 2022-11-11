using System.Linq.Expressions;
using UserManagement.Common.Dto.User;
using UserManagement.Common.Models;

namespace UserManagement.Services.Interfaces;

public interface IUserMapperService
{
    /// <summary>
    /// It maps a filter to an expression.
    /// </summary>
    /// <param name="filter">This is the filter object that is passed in from the client.</param>
    Expression<Func<UserModel, bool>> MapToFilterExpression(UserFilterDto filter);

    /// <summary>
    /// It maps a UserModel to a User
    /// </summary>
    Expression<Func<UserModel, User>> MapToUserDto();
}