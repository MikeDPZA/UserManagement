using UserManagement.Repository.Context;
using UserManagement.Repository.Interfaces;

namespace UserManagement.Repository.Repos;

public class RoleRepository: BaseRepository<UserManagementContext>, IRoleRepository
{
    public RoleRepository(UserManagementContext ctx) : base(ctx)
    {
    }
}