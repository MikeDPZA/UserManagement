using Microsoft.EntityFrameworkCore;
using UserManagement.Common.Models;

namespace UserManagement.Repository.Context;

public class UserManagementContext: DbContext
{
    public UserManagementContext(DbContextOptions<UserManagementContext> options): base(options) { }
    public UserManagementContext(): base() { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }


    public virtual DbSet<UserModel> Users { get; set; }
    public virtual DbSet<UserGroupModel> UserGroups { get; set; }
    public virtual DbSet<GroupModel> Groups { get; set; }
    public virtual DbSet<RoleGroupModel> RoleGroups { get; set; }
    public virtual DbSet<RoleModel> Roles { get; set; }
    public virtual DbSet<RolePermissionModel> RolePermissions { get; set; }
    public virtual DbSet<PermissionModel> PermissionModels { get; set; }
}