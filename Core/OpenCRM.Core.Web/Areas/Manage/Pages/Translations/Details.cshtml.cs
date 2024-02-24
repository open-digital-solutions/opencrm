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
                Page = "Translations",
                Url = "/Manage"
            });
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            var translationModel = await _translationService.GetTranslationAsync<TranslationEntity>(id);
            if (translationModel == null)
            {
                return NotFound();
            }
            Translation = translationModel;
            await _translationService.GetTranslationAsync<TranslationEntity>(id);
            return Page();
        }         
    }
}
