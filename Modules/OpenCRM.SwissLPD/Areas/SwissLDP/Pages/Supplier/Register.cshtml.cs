using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.SwissLPD.Services.SupplierService;

namespace OpenCRM.SwissLPD.Areas.SwissLDP.Pages.Supplier
{
    public class RegisterModel : PageModel
    {
        private RoleService _roleService = new RoleService();

        [BindProperty]
        public RoleModel InputRole { get; set; } = new RoleModel();


        public void OnGet()
        {
        }

        public IActionResult OnPost() 
        {
            if(ModelState.IsValid)
            {
                if(_roleService.ValidateRole(InputRole))
                {
                    //TODO: Registrar el usuarion en la bd
                    var username = InputRole.Email;
                    var userRole = "Supplier";
                    
                    return Redirect("./Index");
                }
                else
                {
                    return Page();
                }
            }
            
            return Page();
        }
    }
}
