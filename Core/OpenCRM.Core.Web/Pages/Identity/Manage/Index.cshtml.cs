using Microsoft.AspNetCore.Http;
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
        public UserEntity Input { get; set; }   
        [BindProperty]
        public IFormFile uploadAvatar { get; set; }

        public IndexModel(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                Input=user;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (ModelState.IsValid)
            {
                user.Name = Input.Name;
                user.Lastname = Input.Lastname;
                if (uploadAvatar != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        uploadAvatar.CopyTo(memoryStream);
                        user.Avatar = memoryStream.ToArray();
                    }
                }

                await _userManager.UpdateAsync(user);
                await _signInManager.RefreshSignInAsync(user);
                //return RedirectToPage("/");
                return RedirectToPage("/Index");
            }

            return Page();
        }

      

    }
}
