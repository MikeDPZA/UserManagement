using UserManagement.Common.Dto;

namespace UserManagement.Repository.Interfaces;

public interface IPermissionRepository
{
    UserPermissionDetailsDto GetUserPermissionDetails(Guid userId);
}