using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Web.Services.LanguageService;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Languages
{
    [Authorize]
    public class EditModel : PageModel
    {
        TranslationModel newTranslationModel;

        private readonly ILanguageService _languageService;

        [BindProperty]
        public LanguageModel<TranslationModel> Language { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        [BindProperty]
        public string JsonData { get; set; } = "";

        public EditModel(ILanguageService languageService)
        {
            newTranslationModel = new TranslationModel
            {
                /* KeyCreate = "",
                 KeyAccept = ""*/
            };

            JsonData = JsonConvert.SerializeObject(newTranslationModel, Formatting.Indented);
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
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            var languageModel = await _languageService.GetLanguageAsync<LanguageEntity>(id);

            if (languageModel == null)
            {
                return NotFound();
            }

            Language = languageModel;
            if (Language != null)
            {
                _ = " \"KeyAccept\" : " + "\"" + Language.Translations?.KeyAccept + "\"";
                _ = " , \"KeyCreate\" : " + "\"" + Language.Translations?.KeyCreate + "\" ";

                JsonData = JsonConvert.SerializeObject(Language.Translations, Formatting.Indented);
            }
            return Page();
        }

        public bool IsValid(string jsonString)
        {
            try
            {
                JObject.Parse(jsonString);
                return true;
            }
            catch (JsonReaderException)
            {
                return false;
            }
        }

        public async Task<IActionResult> OnPost(Guid id)
        {
            TranslationModel? newTranslationModel = new TranslationModel();
            newTranslationModel.KeyAccept = "";
            newTranslationModel.KeyCreate = "";

            if (JsonData != null)
                if (IsValid(JsonData))
                    newTranslationModel = JsonConvert.DeserializeObject<TranslationModel>(JsonData);

            if (ModelState.IsValid)
            {
                var languageModel = await _languageService.GetLanguageAsync<TranslationModel>(id);

                if (languageModel == null)
                {
                    return NotFound();
                }

                var languageModelEdit = new LanguageModel<TranslationModel>()
                {
                    ID = id,
                    Code = Language.Code,
                    Name = Language.Name,
                    Translations = newTranslationModel,
                };
                await _languageService.EditLanguage(languageModelEdit);
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}