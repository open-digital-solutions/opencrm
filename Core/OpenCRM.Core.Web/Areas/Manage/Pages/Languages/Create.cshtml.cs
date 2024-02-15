using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.LanguageService;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Languages
{
    public class CreateModel : PageModel
    {
        readonly TranslationModel newTranslationModel;
        private readonly ILanguageService _languageService;

        [BindProperty]
        public LanguageModel<TranslationModel> Language { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        [BindProperty]
        public string JsonData { get; set; } = "";

        public CreateModel(ILanguageService languageService)
        {
            newTranslationModel = new TranslationModel
            {
                KeyCreate = " ",
                KeyAccept = " "
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

        public async Task<IActionResult> OnPost()
        {
            TranslationModel? newTranslationModel = new()
            {
                KeyAccept = " ",
                KeyCreate = " "
            };

            if (JsonData != null)
                if (IsValid(JsonData))
                    newTranslationModel = JsonConvert.DeserializeObject<TranslationModel>(JsonData);


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