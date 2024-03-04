using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using OpenCRM.Core.Web.Areas.Identity.Models;
using OpenCRM.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OpenCRM.SwissLPD.Services.SupplierService
{
    public class SupplierService : ISupplierService
    {
        public async Task<Tuple<bool, string>> ValidateUserByCHECode(InputRegisterModel input, UserManager<UserEntity> _userManager)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);

            if (user == null)
            {
                var users = _userManager.Users.ToList();
                var inputExtras = JsonSerializer.Deserialize<SupplierModel>(input.UserExtras);

                foreach (var item in users)
                {
                    var userExtras = JsonSerializer.Deserialize<SupplierModel>(item.Data);
                    if(userExtras?.CHECode == inputExtras?.CHECode)
                    {
                        return new Tuple<bool, string>(false, "Supplier with CHE code " + userExtras?.CHECode + " already exists");
                    }
                }
                return new Tuple<bool, string>(true, "");
            }
            
            return new Tuple<bool, string>(false, "User with email " + input.Email + " already exists");
        }
    }
}
