using Microsoft.AspNetCore.Identity;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly UserManager<UserEntity> _userManager;

        public RoleService(RoleManager<RoleEntity> roleManager, UserManager<UserEntity> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IdentityResult> AddRole(string roleName)
        {
            var roleEntity = new RoleEntity
            {
                Name = roleName
            };
            return await _roleManager.CreateAsync(roleEntity);
        }
        public async Task<RoleEntity?> GetRole(string roleName)
        {
            return await _roleManager.FindByNameAsync(roleName);
        }
        public async Task AddToUserRole(UserEntity user)
        {
            await _userManager.AddToRoleAsync(user, Roles.USER_ROLE);
        }
        public async Task AddToAdminRole(UserEntity user)
        {
            await _userManager.AddToRoleAsync(user, Roles.ADMIN_ROLE);
        }
        public async Task AddToSuperAdminRole(UserEntity user)
        {
            await _userManager.AddToRoleAsync(user, Roles.SUPER_ADMIN_ROLE);
        }
        public async Task Seed()
        {
            var superAdminRole = await GetRole(Roles.SUPER_ADMIN_ROLE);
            if (superAdminRole == null)
            {
                await AddRole(Roles.SUPER_ADMIN_ROLE);
            }
            var adminRole = await GetRole(Roles.ADMIN_ROLE);
            if (adminRole == null)
            {
                await AddRole(Roles.ADMIN_ROLE);
            }
            var userRole = await GetRole(Roles.USER_ROLE);
            if (userRole == null)
            {
                await AddRole(Roles.USER_ROLE);
            }
        }
    }
}
