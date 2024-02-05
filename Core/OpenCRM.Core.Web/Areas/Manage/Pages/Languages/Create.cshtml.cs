using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.LanguageService;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Languages
{
    public class CreateModel : PageModel
    {
        private readonly ILanguageService _languageService;

        [BindProperty]
        public LanguageModel<TranslationModel> Language { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public CreateModel(ILanguageService languageService)
        {
            _languageService = languageService;

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
                Area = "Manage",
                IsActive = true,
                Name = "Languages List",
                Page = "Languages",
                Url = "/Manage"
            });

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "Manage",
                IsActive = true,
                Name = "Create Language",
                Page = "Languages",
                Url = "/Manage/Languages/Create"
            });
        }

        public IActionResult OnGet()
        {
            return Page();
        }        

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var languageModel = new LanguageModel<TranslationModel>()
                {
                    ID = Guid.NewGuid(),
                    Code = Language.Code,
                    Name = Language.Name,
                };
                await _languageService.AddLanguage(languageModel);
                return RedirectToPage("./Index");
            }
            return Page();
        }

    }
}
