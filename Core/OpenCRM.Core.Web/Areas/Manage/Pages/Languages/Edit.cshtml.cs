using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.LanguageService;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Languages
{
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
            newTranslationModel = new TranslationModel();

            newTranslationModel.KeyCreate = "";
            newTranslationModel.KeyAccept = "";

            //TODO:  JsonData = JsonConvert.SerializeObject(newTranslationModel, Formatting.Indented);
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
                //TODO: string left = "{ \"KeyAccept\" : " + "\"" + Language.Translations.KeyAccept + "\"";
                //TODO: string right = " , \"KeyCreate\" : " + "\"" + Language.Translations.KeyCreate + "\" }";

                //TODO: JsonData = JsonConvert.SerializeObject(Language.Translations,Formatting.Indented); 
            }
            return Page();
        }

        public bool IsValid(string jsonString)
        {
            try
            {
                //TODO:  JObject.Parse(jsonString);
                return true;
            }
            catch //TODO: (/*JsonReaderException*/)
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
                    // newTranslationModel = JsonConvert.DeserializeObject<TranslationModel>(JsonData);

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
