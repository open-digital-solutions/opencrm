using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.LanguageService;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Languages
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly ILanguageService _languageService;

        [BindProperty]
        public LanguageModel Language { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public DeleteModel(ILanguageService languageService)
        {
            _languageService = languageService;

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "Manage",
                IsActive = true,
                Name = "Languages",
                Page = "Languages",
                Url = "/Manage/Languages"
            });
        }

        public async Task<IActionResult> OnGet(Guid id)
        {            
            var languageModel = await _languageService.GetLanguage(id);

            if (languageModel == null) 
            {
                return NotFound();
            }

            Language = languageModel;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var language = await _languageService.GetLanguage(id);
            
            if (language == null)
            {
                return NotFound();
            }

            await _languageService.DeleteLanguage(id);
            return RedirectToPage("./Index");
        }
    }
}