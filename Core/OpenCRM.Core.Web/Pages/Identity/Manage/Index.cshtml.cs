using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.IdentityService;

namespace OpenCRM.Core.Web.Pages.Identity.Manage
{
    public class IndexModel : PageModel
    {
        private readonly IIdentityService _identityService;
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;

        [BindProperty]
        public UserModel Input { get; set; }

        public IndexModel(IIdentityService identityService)
        {
            _identityService = identityService;
        }
      
        private async Task LoadAsync(UserEntity user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);


            Input = new UserModel
            {

                Name = user.Name,
                Lastname = user.Lastname
            };
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            user.Name = Input.Name;
            user.Lastname = Input.Lastname;
            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            string StatusMessage = "Your profile has been updated";
            ViewData["StatusMessage"] = StatusMessage;

            return RedirectToPage();
        }
    }
}
