using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenCRM.Core.Web.Areas.Identity.Models;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.IdentityService;

namespace OpenCRM.Core.Web.Pages.Identity.Register
{
    public class IndexModel : CorePageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IIdentityService _identityService;
      
        [BindProperty]
        public required InputRegisterModel Input { get; set; }

        [BindProperty]
        public string? ReturnUrl { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IIdentityService identityService)
        {
            _logger = logger;
            _identityService = identityService;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await _identityService.RegisterUser(Input);
                if (result.Item1.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var emailSendResult = await _identityService.SendConfirmationEmail(result.Item2, this);

                    if (emailSendResult)
                    {
                        return RedirectToPage("/Identity/RegisterConfirm/Index", new { email = Input.Email,  returnUrl });
                    }
                    else {
                        //TODO: Change page
                        return RedirectToPage("/Identity/RegisterConfirm/Index", new { email = Input.Email,  returnUrl });
                    }
                }
                foreach (var error in result.Item1.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            else
            {
                Console.WriteLine(ModelState.ErrorCount);
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

    }
}
