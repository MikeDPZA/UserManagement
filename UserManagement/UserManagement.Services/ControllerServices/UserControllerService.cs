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
    private readonly IIdentityService _identityService;

    public UserControllerService(IUserRepository userRepository, IUserMapperService userMapper, IIdentityService identityService)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
        _identityService = identityService;
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
            .GetUsers(_userMapper.MapToFilterExpression(filter) ?? (_ => true))
            .Select(_userMapper.MapToUserDto());
        
        users = ascending 
            ? users.OrderBy(orderBy)
            : users.OrderByDescending(orderBy);
        return new PagedResponse<User>(users, pageNum, pageSize);
    }

    public User GetUser(Guid userId)
    {
        var user = _userRepository.GetUser(_ => _.Id == userId);
        return user == null ? new User() : _userMapper.MapToUserDto(user);
    }
}