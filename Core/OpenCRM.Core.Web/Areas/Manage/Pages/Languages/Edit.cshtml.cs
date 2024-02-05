using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Web.Services.LanguageService;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Languages
{
    public class EditModel : PageModel
    {       
        private readonly ILanguageService _languageService;

        [BindProperty]
        public LanguageModel<TranslationModel> Language { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public EditModel(ILanguageService languageService)
        {
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
            return Page();
        }

        public async Task<IActionResult> OnPost(Guid id)
        {
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
                };                
                await _languageService.EditLanguage(languageModelEdit);
                return RedirectToPage("./Index");                
            }
            return Page();
        }
    }
}
