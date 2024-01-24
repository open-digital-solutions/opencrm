using Microsoft.AspNetCore.Identity;
using OpenCRM.Core.Web.Areas.Identity.Models;
using OpenCRM.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Areas.Identity.Services
{
    public class RegisterService
    {
        public RegisterService()
        {
            
        }

        /// <summary>
        /// For general registration User
        /// </summary>
        /// <param name="Input">Input Register Model to use for user registration</param>
        /// <param name="_userManager"></param>
        /// <param name="_userStore"></param>
        /// <param name="_emailStore"></param>
        /// <returns>A Tuple, first parameter contains the result of the user registration and second
        /// parameter contains the user created</returns>
        public async Task<Tuple<IdentityResult, UserEntity>> RegisterUser(InputRegisterModel Input, UserManager<UserEntity> _userManager, IUserStore<UserEntity> _userStore, IUserEmailStore<UserEntity> _emailStore)
        {
            Console.WriteLine(Input.UserExtras);
            var user = CreateUser();
            if (Input.Name != "")
            {
                user.Name = Input.Name;
            }
            if (Input.Lastname != "")
            {
                user.Lastname = Input.Lastname;
            }
            //Serialize Later the real Extra Properties!!!
            var extra = new { Extra1 = "Extra1", Extra2 = "Extra2" };
            var extraJson = JsonSerializer.Serialize(extra);
            user.UserExtras = extraJson;
            await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, Input.Password);
            return new Tuple<IdentityResult, UserEntity>(result, user);
        }

        private UserEntity CreateUser()
        {
            try
            {
                return Activator.CreateInstance<UserEntity>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(UserEntity)}'. " +
                    $"Ensure that '{nameof(UserEntity)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    }
}
