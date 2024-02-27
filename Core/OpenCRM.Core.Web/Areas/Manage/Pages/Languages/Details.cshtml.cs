using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Web.Services.LanguageService;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Languages
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly ILanguageService _languageService;

        [BindProperty]
        public LanguageModel Language { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public DetailsModel(ILanguageService languageService)
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

        public  async Task<IActionResult> OnGet(Guid id)
        {
            var lanModel = await _languageService.GetLanguage(id);
            
            if (lanModel == null)
            {
                return NotFound();
            }

            Language = lanModel;

            await _languageService.GetLanguage(id);             
            return Page();
        }
    }
}