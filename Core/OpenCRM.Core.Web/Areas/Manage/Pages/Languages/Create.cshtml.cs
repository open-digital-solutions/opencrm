using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.LanguageService;
using System.Text.Json;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Languages
{
    [Authorize]
    public class CreateModel : PageModel
    {
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

        TranslationModel newTranslationModel;
        private readonly ILanguageService _languageService;

        [BindProperty]
        public LanguageModel<TranslationModel> Language { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        [BindProperty]
        public string JsonData { get; set; } = "";

        public CreateModel(ILanguageService languageService)
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

        public async Task<IActionResult> OnPost()
        {
            TranslationModel? newTranslationModel = new TranslationModel();
            newTranslationModel.KeyAccept = "";
            newTranslationModel.KeyCreate = "";

            if (JsonData != null)
                if (IsJsonValid(JsonData))
                {
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
                await _languageService.AddLanguage(languageModel);
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}