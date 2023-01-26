using System.Linq.Expressions;
using UserManagement.Common.Models;
using UserManagement.GraphQL.Types;

namespace UserManagement.GraphQL.Maps;

public class TypeMap
{
    public static Expression<Func<UserModel, UserType>> UserMap = _ => new UserType()
    {
        Email = _.Email,
        Id = _.Id,
        Lastname = _.Lastname,
        Name = _.Name,
        UserIdentifier = _.UserIdentifier,
        // Groups = _.UserGroups.Select(__ => __.Group).Select(GroupMap)
    };
    
    public static Expression<Func<GroupModel, GroupType>> GroupMap = _ => new GroupType()
    {
        Id = _.Id,
        Name = _.Name,
        // Users = _.UserGroups.Select(__ => __.User).Select(UserMap),
        // Roles = _.RoleGroups.Select(__ => __.Role).Select(RoleMap)
    };
    
    public static Expression<Func<RoleModel, RoleType>> RoleMap = _ => new RoleType()
    {
        Id = _.Id,
        Name = _.Name,
        // Groups = _.RoleGroups.Select(__ => __.Group).Select(GroupMap), 
        // Permissions = _.RolePermissions.Select(__ => __.Permission).Select(PermissionMap), 
    };
    
    public static Expression<Func<PermissionModel, PermissionType>> PermissionMap = _ => new PermissionType()
    {
        Id = _.Id,
        Name = _.Name,
        // Roles = _.RolePermissions.Select(__ => __.Role).Select(RoleMap), 
    };
}