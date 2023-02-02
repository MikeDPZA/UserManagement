using System.Linq.Expressions;
using UserManagement.Common.Dto.User;
using UserManagement.Common.Generic;
using UserManagement.Repository.Interfaces;
using UserManagement.Services.Extensions;
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
    public PagedResponse<User> GetUsers(int pageNum, int pageSize, UserFilterDto? filter)
    {
        return GetUsers(pageNum, pageSize, filter, User.SortMap.TryGetValue(filter?.SortKey ?? "", out var orderExp) ? orderExp : _ => _.Firstname);
    }

    /// <inheritdoc/>
    public PagedResponse<User> GetUsers(int pageNum, int pageSize, UserFilterDto? filter, Expression<Func<User, object>> orderBy)
    {
        filter ??= new UserFilterDto();
        var users = _userRepository
            .GetUsers(_userMapper.MapToFilterExpression(filter) ?? (_ => true))
            .Select(_userMapper.MapToUserDto())
            .OrderByDirection(orderBy, filter.SortAscending);
        
        return new PagedResponse<User>(users, pageNum, pageSize);
    }

    public async Task<User> GetUser(Guid userId)
    {
        var user = await _userRepository.GetUserAsync(_ => _.Id == userId);
        return user == null ? new User() : _userMapper.MapToUserDto(user);
    }
}