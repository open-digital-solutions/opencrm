using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using OpenCRM.Core.Web.Areas.Identity.Models;
using OpenDHS.Shared.Data;
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

        public async Task<bool> ValidateRole(InputRegisterModel inputData, IUserStore<UserEntity> userStore)
        {
            var user = await userStore.FindByNameAsync(inputData.Email, new CancellationToken());

            //RoleModel extraData = new RoleModel()
            //{
            //    CHECode = "CHE-123.456.789 MWST",
            //    Name = "Sandra",
            //    Role = "Supplier",
            //    Address = "Ermita 216",
            //    Phone = "676",
            //    Mobile = "7881"
            //};

            //var userResult = new UserEntity()
            //{
            //    UserName = "",
            //    Email = "sandrahdez@gmail.com", //grettelhernandez@gmail.com
            //    UserExtras = JsonSerializer.Serialize(extraData)
            //};

            if (user != null)
            {
                var inputExtras = JsonSerializer.Deserialize<RoleModel>(inputData.UserExtras);
                var userExtras = JsonSerializer.Deserialize<RoleModel>(user.UserExtras);

                if (inputExtras?.CHECode == userExtras?.CHECode)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
