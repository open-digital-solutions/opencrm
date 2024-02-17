using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.LanguageService;
using System.Text.Json;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Languages
{
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

            newTranslationModel.KeyCreate = "";
            newTranslationModel.KeyAccept = "";

            JsonData = JsonSerializer.Serialize(newTranslationModel, options);

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
                JsonData = JsonSerializer.Serialize(Language.Translations, options);
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

        public bool IsKeyAcceptValid(string jsonString)
        {
            try
            {
                JsonDocument document = JsonDocument.Parse(jsonString);
                document.RootElement.GetProperty("KeyAccept");
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool IsKeyCreateValid(string jsonString)
        {
            try
            {
                JsonDocument document = JsonDocument.Parse(jsonString);
                document.RootElement.GetProperty("KeyCreate");
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public string getKeyCreate(string jsonString)
        {
            JsonDocument document = JsonDocument.Parse(jsonString);
            var rootElement = document.RootElement;
            return rootElement.GetProperty("KeyCreate").GetString();
        }

        public string getKeyAccept(string jsonString)
        {
            JsonDocument document = JsonDocument.Parse(jsonString);
            var rootElement = document.RootElement;
            return rootElement.GetProperty("KeyAccept").GetString();
        }

        public string getMissKey(string jsonString)
        {
            List<String> keys = new List<String>();
            keys.Add("KeyAccept");
            keys.Add("KeyCreate");

            JsonDocument document = JsonDocument.Parse(jsonString);
            var rootElement = document.RootElement;
            var all = rootElement.EnumerateObject();
            return rootElement.EnumerateObject().Where(x => !keys.Contains(x.Name)).First<JsonProperty>().Value.GetString();
        }

        public string getMissKeybyIndex(string jsonString, int index)
        {
            JsonDocument document = JsonDocument.Parse(jsonString);
            var rootElement = document.RootElement;

            return rootElement.EnumerateObject().ToList<JsonProperty>().ElementAt<JsonProperty>(index).Value.GetString();
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
                        if (IsKeyAcceptValid(JsonData) && IsKeyCreateValid(JsonData))
                        {
                            newTranslationModel = JsonSerializer.Deserialize<TranslationModel>(JsonData);
                        }
                        else if (IsKeyAcceptValid(JsonData) && !IsKeyCreateValid(JsonData))
                        {
                            newTranslationModel.KeyAccept = getKeyAccept(JsonData);
                            newTranslationModel.KeyCreate = getMissKey(JsonData);
                        }
                        else if (!IsKeyAcceptValid(JsonData) && IsKeyCreateValid(JsonData))
                        {
                            newTranslationModel.KeyAccept = getMissKey(JsonData);
                            newTranslationModel.KeyCreate = getKeyCreate(JsonData);
                        }
                        else if (!IsKeyAcceptValid(JsonData) && !IsKeyCreateValid(JsonData))
                        {
                            newTranslationModel.KeyAccept = getMissKeybyIndex(JsonData, 0);
                            newTranslationModel.KeyCreate = getMissKeybyIndex(JsonData, 1);
                        }
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