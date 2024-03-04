using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.UI.V5.Pages.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OpenCRM.Core.Web.Areas.Identity.Models;
using OpenCRM.Core.Web.Areas.Identity.Services;
using OpenCRM.SwissLPD.Services.SupplierService;
using OpenCRM.Core.Data;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using OpenCRM.Core.Web.Models;
using System.Xml.Linq;

namespace OpenCRM.SwissLPD.Areas.SwissLDP.Pages.Supplier
{
    public class RegisterModel : PageModel
    {
        private ISupplierService _supplierService;

        private readonly UserManager<UserEntity> _userManager;

        private readonly IUserStore<UserEntity> _userStore;

        private readonly IUserEmailStore<UserEntity> _emailStore;

        private RegisterService _registerService = new RegisterService();

        public RegisterModel(UserManager<UserEntity> userManager, IUserStore<UserEntity> userStore, ISupplierService supplierService)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = (IUserEmailStore<UserEntity>)_userStore;
            _supplierService = supplierService;

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "SwissLDP",
                IsActive = true,
                Name = "Suppliers",
                Page = "Supplier",
                Url = "/SwissLDP/Supplier"
            });
        }

        public string ValidateError { get; set; } = string.Empty;

        [BindProperty]
        public InputRegisterModel InputUser { get; set; } = default!;

        [BindProperty]
        public SupplierModel InputRoleData { get; set; } = new SupplierModel();

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public IActionResult OnGet()
        {
            return Page();
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

                Tuple<bool, string> validateResult = await _supplierService.ValidateUserByCHECode(user, _userManager);
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
