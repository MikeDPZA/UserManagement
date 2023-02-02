using System.Linq.Expressions;
using UserManagement.Common.Dto.Permission;
using UserManagement.Common.Generic;
using UserManagement.Repository.Interfaces;
using UserManagement.Services.Extensions;
using UserManagement.Services.Interfaces;

namespace UserManagement.Services.ControllerServices;

public class PermissionControllerService : IPermissionControllerService
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IPermissionMapperService _permissionMapper;

    public PermissionControllerService(IPermissionRepository permissionRepository, IPermissionMapperService permissionMapper)
    {
        _permissionRepository = permissionRepository;
        _permissionMapper = permissionMapper;
    }

    public PagedResponse<Permission> GetPermissions(int pageNum, int pageSize)
    {
        return GetPermissions(pageNum, pageSize, null);
    }

    public PagedResponse<Permission> GetPermissions(int pageNum, int pageSize, PermissionFilterDto? filter)
    {
        return GetPermissions(pageNum, pageSize, filter,
            Permission.SortMap.TryGetValue(filter?.SortKey ?? "", out var orderBy) ? orderBy : (_ => _.Name));
    }

    public PagedResponse<Permission> GetPermissions(int pageNum, int pageSize, PermissionFilterDto? filter,
        Expression<Func<Permission, object>> orderBy)
    {
        filter ??= new PermissionFilterDto();
        var permissions = _permissionRepository
            .GetPermissions(_permissionMapper.MapToFilterExpression(filter))
            .Select(_permissionMapper.MapToPermissionDto())
            .OrderByDirection(orderBy, filter.SortAscending);

        return new PagedResponse<Permission>(permissions, pageNum, pageSize);
    }
}