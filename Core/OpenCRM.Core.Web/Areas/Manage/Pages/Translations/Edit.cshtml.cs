using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Web.Services.TranslationService;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Translations
{
    public class EditModel : PageModel
    {
        private readonly ITranslationService _translationService;

        [BindProperty]
        public TranslationModel<TranslationEntity> Translation { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public EditModel(ITranslationService translationService) 
        {
            _translationService = translationService;

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
                Name = "Translation List",
                Page = "Translation",
                Url = "/Manage"
            });
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            var transModel = await _translationService.GetTranslationAsync<TranslationEntity>(id);

            if (transModel == null)
            {
                return NotFound();
            }
            Translation = transModel;
            return Page();
        }

        public async Task<IActionResult> OnPost(Guid id)
        {
            if (ModelState.IsValid)
            {
                var transModel = await _translationService.GetTranslationAsync<TranslationEntity>(id);

                if (transModel == null)
                {
                    return NotFound();
                }

                var transModelEdit = new TranslationModel<TranslationEntity>()
                {
                    ID = id,
                    Key = Translation.Key,
                    LanguageId = Translation.LanguageId,
                    Translation = Translation.Translation                    
                };
                await _translationService.EditTranslation(transModelEdit);
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
