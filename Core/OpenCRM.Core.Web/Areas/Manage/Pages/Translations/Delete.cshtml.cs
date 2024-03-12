using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.TranslationService;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Translations
{
	public class DeleteModel : PageModel
    {
        private readonly ITranslationService _translationService;

        [BindProperty]
        public TranslationModel TranslationModel { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public DeleteModel(ITranslationService translationService)
        {
            _translationService = translationService;

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "Manage",
                IsActive = true,
                Name = "Translations",
                Page = "Translations",
                Url = "/Manage/Translations"
            });
        }        

        public async Task<IActionResult> OnGet(Guid id)
        {
            var key = await _translationService.GetTranslationKey(id);

            if (key == null)
            {
                return NotFound();
            }

            var translations = _translationService.GetTranslationsByKey(key);

            if (translations == null)
            {
                return NotFound();
            }

            var model = new TranslationModel()
            {
                Key = key,
                Translations = translations
            };

            TranslationModel = model;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            await _translationService.DeleteTranslation(TranslationModel.Key);
            return RedirectToPage("./Index");
        }
    }
}
