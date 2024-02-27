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
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

         TranslationModel newTranslationModel;

        private readonly ILanguageService _languageService;
        private readonly ITranslationService _translationService;

        [BindProperty]
        public LanguageModel<TranslationModel> Language { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        [BindProperty]
        public string JsonData { get; set; } = "";

        public CreateModel(ILanguageService languageService , ITranslationService translationService)
        {
            _translationService = translationService;
            newTranslationModel = new TranslationModel();

            JsonData = JsonSerializer.Serialize(newTranslationModel.Translations, options);
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
        public bool IsJsonValid(string jsonString)
        {
            try
            {
                JsonDocument document = JsonDocument.Parse(jsonString);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }


        public void addNewKeyToTranslationInLanguages(LanguageModel<TranslationModel> language, String key, String value = "")
        {
            Dictionary<string, string> keys = language.Translations.Translations;
            if (!keys.ContainsKey(key))
            {
                keys[key] = value;
            }
        }


        public async Task<IActionResult> OnPost()
        {
            TranslationModel? newTranslationModel = new TranslationModel();
        
            if (JsonData != null)
                if (IsJsonValid(JsonData))
                {
                    
                        newTranslationModel.Translations = JsonSerializer.Deserialize< Dictionary<String,String> >(JsonData);
                }

            if (ModelState.IsValid)
            {
                var languageModel = new LanguageModel<TranslationModel>()
                {
                    ID = Guid.NewGuid(),
                    Code = Language.Code,
                    Name = Language.Name,
                    Translations = newTranslationModel,
                };
                
                var translationDB = _translationService.GetTranslationListAsync<TranslationEntity>();
                foreach (var translation in translationDB)
                {
                    addNewKeyToTranslationInLanguages(languageModel, translation.Key, "");
                }
                
                await _languageService.AddLanguage(languageModel);
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}