using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Services.TranslationService;
using Microsoft.AspNetCore.Mvc.Rendering;
using OpenCRM.Core.Web.Services.LanguageService;
using OpenCRM.Core.Web.Models;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Translations
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ITranslationService _translationService;
        private readonly ILanguageService _languageService;

        [BindProperty]
        public TranslationModel<TranslationEntity> Translation { get; set; } = default!;


        [BindProperty]
        public List<String> TranslationValues { get; set; } = new List<string>();
        
        [BindProperty]
        public List<SelectListItem> Languages { set; get; }

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();



        public CreateModel(ITranslationService translationService, ILanguageService languageService)
        {
            
            _translationService = translationService;
            _languageService = languageService;
            var LanguagesDB = _languageService.GetLanguageListAsync<TranslationModel>();
            Languages = LanguagesDB.Select(f => new SelectListItem { Text = f.Name, Value = f.ID.ToString() }).ToList();

            foreach (var language in LanguagesDB)
            {
                TranslationValues.Add("");
            }

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



        public void addNewKeyToTranslationInLanguages(LanguageModel<TranslationModel> language, String key , String value="")
        {
            Dictionary<string, string> keys = language.Translations.Translations;
            if (!keys.ContainsKey(key))
            {
                keys[key] = value;
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var translationModel = new TranslationModel<TranslationEntity>()
                {
                    ID = Translation.ID,
                    Key = Translation.Key,
                    Translation = "",
                    LanguageId = Translation.LanguageId,
                };
                await _translationService.AddTranslation(translationModel);

                var LanguagesDB = _languageService.GetLanguageListAsync<TranslationModel>();
                //int index = 0; , TranslationValues.ElementAt<string>(index++)
                foreach (var language in LanguagesDB)
                {
                    addNewKeyToTranslationInLanguages(language, Translation.Key);
                    await _languageService.EditLanguage(language);
                }


                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
