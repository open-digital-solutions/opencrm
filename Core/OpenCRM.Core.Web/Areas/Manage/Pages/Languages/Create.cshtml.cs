using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.LanguageService;
using OpenCRM.Core.Web.Services.TranslationService;
using System.Text.Json;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Languages
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ILanguageService _languageService;

        private readonly ITranslationService _translationService;

        [BindProperty]
        public LanguageModel Language { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public CreateModel(ILanguageService languageService, ITranslationService translationService)
        {
            _languageService = languageService;
            _translationService = translationService;

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "Manage",
                IsActive = true,
                Name = "Languages",
                Page = "Languages",
                Url = "/Manage/Languages"
            });
        }

        public IActionResult OnGet()
        {
            var keyTranslations = _translationService.GetKeyTranslationsByLanguage<TranslationModel<TranslationEntity>>();
            return Page();
        }
       
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var languageModel = new LanguageModel()
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