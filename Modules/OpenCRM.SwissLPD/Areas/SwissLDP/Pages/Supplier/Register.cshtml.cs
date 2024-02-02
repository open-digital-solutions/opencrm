using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Areas.Identity.Models;
using OpenCRM.Core.Web.Areas.Identity.Services;
using OpenCRM.SwissLPD.Services.SupplierService;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using OpenCRM.Core.Web.Models;
using System.Xml.Linq;

namespace OpenCRM.SwissLPD.Areas.SwissLDP.Pages.Supplier
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<UserEntity> _userManager;

        private readonly IUserStore<UserEntity> _userStore;

        private readonly IUserEmailStore<UserEntity> _emailStore;

        private RoleService _roleService = new RoleService();

        private RegisterService _registerService = new RegisterService();

        public RegisterModel(UserManager<UserEntity> userManager, IUserStore<UserEntity> userStore)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = (IUserEmailStore<UserEntity>)_userStore;

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Home",
                Page = "",
                Url = "/"
            });

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "SwissLDP",
                IsActive = true,
                Name = "Supplier",
                Page = "Supplier",
                Url = "/SwissLDP/Supplier"
            });

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "SwissLDP",
                IsActive = true,
                Name = "Register",
                Page = "Event",
                Url = "/SwissLDP/Supplier/Register"
            });
        }

        public string ValidateError { get; set; } = string.Empty;

        [BindProperty]
        public InputRegisterModel InputUser { get; set; }

        [BindProperty]
        public RoleData InputRoleData { get; set; } = new RoleData();

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
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

                Tuple<bool, string> validateResult = await _roleService.ValidateUserByCHECode(user, _userManager);
                ValidateError = validateResult.Item2;

                if (validateResult.Item1)
                {
                    var result = await _registerService.RegisterUser(user, _userManager, _userStore, _emailStore);

                    if (result.Item1.Succeeded)
                    {
                        return Redirect("./Index");
                    }
                }
            }
            return Page();
        }
    }
}
