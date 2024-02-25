using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.LanguageService;
using System.Text.Json;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Languages
{
    [Authorize]
    public class EditModel : PageModel
    {
        TranslationModel newTranslationModel;
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

        private readonly ILanguageService _languageService;

        [BindProperty]
        public LanguageModel<TranslationModel> Language { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        [BindProperty]
        public string JsonData { get; set; } = "";

        public EditModel(ILanguageService languageService)
        {
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
                JsonData = JsonSerializer.Serialize(Language.Translations.Translations, options);
            }
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

       
        public async Task<IActionResult> OnPost(Guid id)
        {
            TranslationModel? newTranslationModel = new TranslationModel();
            if (ModelState.IsValid)
            {
                var languageModel = await _languageService.GetLanguageAsync<TranslationModel>(id);
                if (languageModel == null)
                {
                    return NotFound();
                }
                if (JsonData != null)
                    if (IsJsonValid(JsonData))
                        newTranslationModel.Translations = JsonSerializer.Deserialize < Dictionary<String,String>>(JsonData);
                      else
                         newTranslationModel = languageModel.Translations;

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