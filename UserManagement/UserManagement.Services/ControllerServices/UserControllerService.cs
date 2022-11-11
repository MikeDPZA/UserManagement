using System.Linq.Expressions;
using UserManagement.Common.Dto.User;
using UserManagement.Common.Generic;
using UserManagement.Repository.Interfaces;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.ControllerServices;

public class UserControllerService: IUserControllerService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserMapperService _userMapper;

    public UserControllerService(IUserRepository userRepository, IUserMapperService userMapper)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
    }

    /// <inheritdoc/>
    public PagedResponse<User> GetUsers(int pageNum, int pageSize)
    {
        return GetUsers(pageNum, pageSize, null);
    }

    /// <inheritdoc/>
    public PagedResponse<User> GetUsers(int pageNum, int pageSize, UserFilterDto filter)
    {
        return GetUsers(pageNum, pageSize, filter, _ => _.Firstname);
    }

    /// <inheritdoc/>
    public PagedResponse<User> GetUsers(int pageNum, int pageSize, UserFilterDto filter, Expression<Func<User, object>> orderBy, bool ascending = true)
    {
        var users = _userRepository
            .GetUsers(_userMapper.MapToFilterExpression(filter))
            .Select(_userMapper.MapToUserDto());
        
        users = ascending 
            ? users.OrderBy(orderBy)
            : users.OrderByDescending(orderBy);
        
        return new PagedResponse<User>(users, pageNum, pageSize);
    }
}