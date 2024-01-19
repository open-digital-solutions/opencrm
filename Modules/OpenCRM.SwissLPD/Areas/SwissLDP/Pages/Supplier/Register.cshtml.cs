using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OpenCRM.Core.Web.Areas.Identity.Models;
using OpenCRM.Core.Web.Areas.Identity.Services;
using OpenCRM.SwissLPD.Services.SupplierService;
using OpenCRM.Core.Data;
using System.Text.Json;

namespace OpenCRM.SwissLPD.Areas.SwissLDP.Pages.Supplier
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<UserEntity> _userManager;

        private readonly IUserStore<UserEntity> _userStore;
        
        private readonly IUserEmailStore<UserEntity> _emailStore;


        private RoleService _roleService = new RoleService();

        private RegisterService _registerService = new RegisterService();

        public RegisterModel(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, IUserStore<UserEntity> userStore)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = (IUserEmailStore<UserEntity>)_userStore;
        }

        [BindProperty]
        public InputRegisterModel InputUser { get; set; }

        [BindProperty]
        public RoleData InputRoleData { get; set; } = new RoleData();

        [BindProperty]
        public bool IsValidInput { get; set; } = true;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync() 
        {
            if(ModelState.IsValid)
            {
                InputRoleData.Role = "Supplier";

                var user = new InputRegisterModel()
                {
                    Name = InputUser.Name,
                    Email = InputUser.Email,
                    Password = InputUser.Password,
                    ConfirmPassword = InputUser.ConfirmPassword,
                    UserExtras = JsonSerializer.Serialize(InputRoleData)
                };

                if(await _roleService.ValidateUserByCHECode(user, _userManager))
                {
                    var result = await _registerService.RegisterUser(user, _userManager, _userStore, _emailStore);
                 
                    if(result.Item1.Succeeded)
                    {
                        IsValidInput = true;
                        return Redirect("./Index");
                    }
                }
                else
                {
                    IsValidInput = false;
                    return Page();
                }
            }
            
            return Page();
        }
    }
}
