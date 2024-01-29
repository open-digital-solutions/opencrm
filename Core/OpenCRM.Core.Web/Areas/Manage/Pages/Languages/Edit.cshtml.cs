using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Web.Services.LanguageService;
using OpenCRM.Core.Data;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Languages
{
    public class EditModel : PageModel
    {       
        private readonly ILanguageService _languageService;
        public EditModel(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [BindProperty]
        public LanguageModel<LanguageEntity> Language { get; set; } = default!;

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
                var languageModel = await _languageService.GetLanguageAsync<LanguageEntity>(id);

                if (languageModel == null)
                {
                    return NotFound();
                }

                var languageModelEdit = new LanguageModel<LanguageEntity>()
                {
                    ID = id,
                    Code = Language.Code,
                    Name = Language.Name,                   
                };                
                await _languageService.EditLanguage(languageModelEdit);
                //return RedirectToAction("./Index");                
            }
            return Page();
        }
    }
}
