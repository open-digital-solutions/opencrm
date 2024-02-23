using Microsoft.AspNetCore.Identity;
using OpenCRM.Core.Data;

namespace OpenCRM.Core.Web.Services.RoleService
{
    public interface IRoleService
    {
        Task<IdentityResult> AddRole(string roleName);
        Task AddToAdminRole(UserEntity user);
        Task AddToSuperAdminRole(UserEntity user);
        Task AddToUserRole(UserEntity user);
        Task<RoleEntity?> GetRole(string roleName);
        Task Seed();
    }
}