using Microsoft.EntityFrameworkCore;
using UserManagement.Common.Models;
using UserManagement.GraphQL.Maps;
using UserManagement.GraphQL.Types;
using UserManagement.Repository.Interfaces;

namespace UserManagement.GraphQL.Schema;

public class Query
{
    private readonly IUserRepository _userRepository;
    private readonly IGroupRepository _groupRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;

    public Query(IUserRepository userRepository, IGroupRepository groupRepository, IRoleRepository roleRepository,
        IPermissionRepository permissionRepository)
    {
        _userRepository = userRepository;
        _groupRepository = groupRepository;
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
    }

    public async Task<List<UserType>> GetUsers()
        => await _userRepository.GetUsers().Select(user => new UserType()
        {
            Email = user.Email,
            Id = user.Id,
            Lastname = user.Lastname,
            Name = user.Name,
            UserIdentifier = user.UserIdentifier,
            Groups = user.UserGroups.Select(__ => __.Group).Select(group => new GroupType()
            {
                Id = group.Id,
                Name = group.Name,
                Roles = group.RoleGroups.Select(__ => __.Role).Select(role => new RoleType()
                {
                    Id = role.Id,
                    Name = role.Name,
                    Permissions = role.RolePermissions.Select(__ => __.Permission).Select(permission => new PermissionType()
                    {
                        Id = permission.Id,
                        Name = permission.Name,
                        Key = permission.Key,
                    })
                })
            })
            
        }).ToListAsync();

    public async Task<List<GroupType>> GetGroups()
        => await _groupRepository.GetGroups().Select(group => new GroupType()
        {
            Id = group.Id,
            Name = group.Name,
            Users = group.UserGroups.Select(__ => __.User).Select(__ =>new UserType()
            {
                Email = __.Email,
                Id = __.Id,
                Lastname = __.Lastname,
                Name = __.Name,
                UserIdentifier = __.UserIdentifier
            }),
            Roles = group.RoleGroups.Select(__ => __.Role).Select(role => new RoleType()
            {
                Id = role.Id,
                Name = role.Name,
                Permissions = role.RolePermissions.Select(__ => __.Permission).Select(permission => new PermissionType()
                {
                    Id = permission.Id,
                    Name = permission.Name,
                    Key = permission.Key,
                })
            })
        }).ToListAsync();

    public async Task<List<RoleType>> GetRoles()
        => await _roleRepository.GetRoles().Select(role => new RoleType()
        {
            Id = role.Id,
            Name = role.Name,
            Groups = role.RoleGroups.Select(__ => __.Group).Select(group => new GroupType()
            {
                Id = group.Id,
                Name = group.Name,
                Users = group.UserGroups.Select(__ => __.User).Select(user =>new UserType()
                {
                    Email = user.Email,
                    Id = user.Id,
                    Lastname = user.Lastname,
                    Name = user.Name,
                    UserIdentifier = user.UserIdentifier
                })
            }), 
            Permissions = role.RolePermissions.Select(__ => __.Permission).Select(permission => new PermissionType()
            {
                Id = permission.Id,
                Name = permission.Name,
                Key = permission.Key,
            }), 
        }).ToListAsync();

    public async Task<List<PermissionType>> GetPermissions()
        => await _permissionRepository.GetPermissions().Select(permission => new PermissionType()
        {
            Id = permission.Id,
            Name = permission.Name,
            Key = permission.Key,
            Roles = permission.RolePermissions.Select(__ => __.Role).Select(role => new RoleType()
            {
                Id = role.Id,
                Name = role.Name,
                Groups = role.RoleGroups.Select(__ => __.Group).Select(group => new GroupType()
                {
                    Id = group.Id,
                    Name = group.Name,
                    Users = group.UserGroups.Select(__ => __.User).Select(user =>new UserType()
                    {
                        Email = user.Email,
                        Id = user.Id,
                        Lastname = user.Lastname,
                        Name = user.Name,
                        UserIdentifier = user.UserIdentifier
                    })
                })
            }), 
        }).ToListAsync();
}