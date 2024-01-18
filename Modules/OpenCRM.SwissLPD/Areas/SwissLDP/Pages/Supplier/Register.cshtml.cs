using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OpenCRM.Core.Web.Areas.Identity.Models;
using OpenCRM.Core.Web.Areas.Identity.Services;
using OpenCRM.SwissLPD.Services.SupplierService;
using OpenDHS.Shared.Data;
using System.Text.Json;

namespace OpenCRM.SwissLPD.Areas.SwissLDP.Pages.Supplier
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<UserEntity> _signInManager;

        private readonly UserManager<UserEntity> _userManager;

        private readonly IUserStore<UserEntity> _userStore;
        
        private readonly IUserEmailStore<UserEntity> _emailStore;


        private RoleService _roleService = new RoleService();

        private RegisterService _registerService = new RegisterService();

        public RegisterModel(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, IUserStore<UserEntity> userStore, IUserEmailStore<UserEntity> emailStore)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = emailStore;
        }

        [BindProperty]
        public RoleModel InputRole { get; set; } = new RoleModel();

        [BindProperty]
        public bool IsValidInput { get; set; } = true;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync() 
        {
            if(ModelState.IsValid)
            {

                RoleModel extraData = new RoleModel()
                {
                    CHECode = InputRole.CHECode,
                    Name = InputRole.Name,
                    Role = "Supplier",
                    Address = InputRole.Address,
                    Phone = InputRole.Phone,
                    Mobile = InputRole.Mobile
                };

                var inputData = new InputRegisterModel()
                {
                    Email = InputRole.Email,
                    UserExtras = JsonSerializer.Serialize(extraData)
                };

                if(await _roleService.ValidateRole(inputData, _userStore))
                {
                    //TODO: Registrar el usuarion en la bd
                    IsValidInput = true;
                    var result = await _registerService.RegisterUser(inputData, _userManager, _userStore, _emailStore);
                    return Redirect("./Index");
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
