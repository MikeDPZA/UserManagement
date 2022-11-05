using System.Linq.Expressions;
using UserManagement.Common.Dto.User;
using UserManagement.Common.Generic;
using UserManagement.Common.Models;
using UserManagement.ControllerServices.Interfaces;
using UserManagement.Repository.Interfaces;

namespace UserManagement.ControllerServices.Services;

public class UserControllerService: IUserControllerService
{
    private readonly IUserRepository _userRepository;

    public UserControllerService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <inheritdoc/>
    public PagedResponse<User> GetUsers(int pageNum, int pageSize)
    {
        return GetUsers(pageNum, pageSize, null);
    }

    /// <inheritdoc/>
    public PagedResponse<User> GetUsers(int pageNum, int pageSize, Expression<Func<UserModel, bool>> filter)
    {
        return GetUsers(pageNum, pageSize, null, _ => _.Firstname);
    }

    /// <inheritdoc/>
    public PagedResponse<User> GetUsers(int pageNum, int pageSize, Expression<Func<UserModel, bool>> filter, Expression<Func<User, object>> orderBy, bool ascending = true)
    {
        var users = _userRepository
            .GetUsers(filter)
            .Select(_ => new User()
            {
                Email = _.Email,
                Firstname = _.Name,
                Lastname = _.Lastname,
                Id = _.Id,
                UserIdentifier = _.UserIdentifier
            });
        
        users = ascending 
            ? users.OrderBy(orderBy)
            : users.OrderByDescending(orderBy);
        
        return new PagedResponse<User>(users, pageNum, pageSize);
    }
}