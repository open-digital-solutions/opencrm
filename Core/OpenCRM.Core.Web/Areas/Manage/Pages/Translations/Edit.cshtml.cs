using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Web.Services.TranslationService;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using OpenCRM.Core.Web.Services.LanguageService;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Translations
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ITranslationService _translationService;
        private readonly ILanguageService _languageService;

        [BindProperty]
        public TranslationModel<TranslationEntity> Translation { get; set; } = default!;

        [BindProperty]
        public List<SelectListItem> Languages { set; get; }


        [BindProperty]
        public List<String> TranslationValues { get; set; } = new List<string>();

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public EditModel(ITranslationService translationService, ILanguageService languageService)
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
                Name = "Translation List",
                Page = "Translation",
                Url = "/Manage"
            });
        }

        public string getValueByKey(LanguageModel<TranslationModel> language, string key)
        {
            if (language.Translations.Translations.ContainsKey(key))
                return language.Translations.Translations[key];
            return "";
        }
        public async Task<IActionResult> OnGet(Guid id)
        {
            var transModel = await _translationService.GetTranslationAsync<TranslationEntity>(id);

            if (transModel == null)
            {
                return NotFound();
            }
            var LanguagesDB = _languageService.GetLanguageListAsync<TranslationModel>();
            Languages = LanguagesDB.Select(f => new SelectListItem { Text = f.Name, Value = f.ID.ToString() }).ToList();

            int index = 0;
            foreach (var language in LanguagesDB)
            {
                if( index < TranslationValues.Count )
                TranslationValues[index++] = getValueByKey(language, transModel.Key);
            }

            Translation = transModel;
            return Page();
        }
        public void editValueKeyToTranslationInLanguages(LanguageModel<TranslationModel> language, String key, String value = "")
        {
            Dictionary<string, string> keys = language.Translations.Translations;
            if (keys.ContainsKey(key))
            {
                keys[key] = value;
            }
        }

        public async Task<IActionResult> OnPost(Guid id)
        {
            if (ModelState.IsValid)
            {
                var transModel = await _translationService.GetTranslationAsync<TranslationEntity>(id);

                if (transModel == null)
                {
                    return NotFound();
                }

                var transModelEdit = new TranslationModel<TranslationEntity>()
                {
                    ID = id,
                    Key = Translation.Key,
                    LanguageId = Translation.LanguageId,
                    Translation = Translation.Translation
                };
                await _translationService.EditTranslation(transModelEdit);
                var LanguagesDB = _languageService.GetLanguageListAsync<TranslationModel>();
                
                int index = 0;
                foreach (var language in LanguagesDB)
                {
                    editValueKeyToTranslationInLanguages(language, Translation.Key, TranslationValues.ElementAt<string>(index++));
                    await _languageService.EditLanguage(language);
                }

                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
