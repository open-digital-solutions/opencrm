using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using OpenCRM.Core.Web.Areas.Identity.Models;
using OpenCRM.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OpenCRM.SwissLPD.Services.SupplierService
{
    public class RoleService
    {
        public async Task<bool> ValidateUserByCHECode(InputRegisterModel input, UserManager<UserEntity> _userManager)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);
            
            if (user != null)
            {
                var inputExtras = JsonSerializer.Deserialize<RoleData>(input.UserExtras);
                var userExtras = JsonSerializer.Deserialize<RoleData>(user.UserExtras);

                if (inputExtras?.CHECode == userExtras?.CHECode)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
