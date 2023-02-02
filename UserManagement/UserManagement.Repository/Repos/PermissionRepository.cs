using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UserManagement.Common.Dto;
using UserManagement.Common.Exceptions;
using UserManagement.Common.Models;
using UserManagement.Repository.Context;
using UserManagement.Repository.Interfaces;

namespace UserManagement.Repository.Repos;

public class PermissionRepository : BaseRepository<PermissionModel>, IPermissionRepository
{
    public PermissionRepository(UserManagementContext ctx) : base(ctx)
    {
    }

    public UserPermissionDetailsDto GetUserPermissionDetails(Guid userId)
    {
        var user = Ctx.Users
            .Include(_ => _.UserGroups)
            .ThenInclude(_ => _.Group)
            .ThenInclude(_ => _.RoleGroups)
            .ThenInclude(_ => _.Role)
            .ThenInclude(_ => _.RolePermissions)
            .ThenInclude(_ => _.Permission)
            .FirstOrDefault(_ => _.Id == userId && !_.IsDeleted);
        
        if (user == null)
            throw new UserNotExistException($"User with id: {userId} does not exist");

        var userPermissions = user.UserGroups.Select(_ => _.Group).Where(_ => !_.IsDeleted)
                                                   .SelectMany(_ => _.RoleGroups)
                                                   .Select(_ => _.Role).Where(_ => !_.IsDeleted)
                                                   .SelectMany(_ => _.RolePermissions)
                                                   .Select(_ => _.Permission).Where(_ => !_.IsDeleted)
                                                   .Select(_ => _.Key)
                                                   .ToArray();
        return new UserPermissionDetailsDto()
        {
            Email = user.Email,
            Firstname = user.Name,
            Lastname = user.Lastname,
            Permissions = userPermissions,
            UserId = user.Id,
            UserIdentifier = user.UserIdentifier
        };
    }

    public IQueryable<PermissionModel> GetPermissions(Expression<Func<PermissionModel, bool>> filter = null)
        => GetQueryable(filter);
}