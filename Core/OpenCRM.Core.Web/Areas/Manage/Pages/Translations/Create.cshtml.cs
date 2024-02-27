using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Services.TranslationService;
using Microsoft.AspNetCore.Mvc.Rendering;
using OpenCRM.Core.Web.Services.LanguageService;
using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Translations
{
    public class CreateModel : PageModel
    {
        private readonly ITranslationService _translationService;
        private readonly ILanguageService _languageService;

        [BindProperty]
        public TranslationModel<TranslationEntity> Translation { get; set; } = default!;
        public List<SelectListItem> Languages { set; get; }

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public CreateModel(ITranslationService translationService , ILanguageService languageService)
        {
            _translationService = translationService;
            _languageService = languageService;
            var LanguagesDB = _languageService.GetLanguageListAsync();
            Languages = LanguagesDB.Select(f => new SelectListItem { Text = f.Name, Value = f.ID.ToString() }).ToList();

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
                Name = "Translations List",
                Page = "Translations",
                Url = "/Manage"
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
                var translationModel = new TranslationModel<TranslationEntity>()
                {
                    ID = Translation.ID,
                    Key = Translation.Key,
                    Translation = Translation.Translation,
                    LanguageId = Translation.LanguageId,                 
                };                
                await _translationService.AddTranslation(translationModel);
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
