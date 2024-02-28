using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.TranslationService;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Translations
{
    public class DetailsModel : PageModel
    {        
        private readonly ITranslationService  _translationService;

        [BindProperty]
        public TranslationModel<TranslationEntity> Translation { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public DetailsModel(ITranslationService translationService)
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
            var translationModel = await _translationService.GetTranslationByIdAsync<TranslationEntity>(id);
            
            if (translationModel == null)
            {
                return NotFound();
            }
            
            Translation = translationModel;
            
            await _translationService.GetTranslationByIdAsync<TranslationEntity>(id);
            return Page();
        }         
    }
}
