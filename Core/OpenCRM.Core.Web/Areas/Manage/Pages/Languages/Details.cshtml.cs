using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Web.Services.LanguageService;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Languages
{
    public class DetailsModel : CorePageModel
    {
        private readonly ILanguageService _languageService;

        [BindProperty]
        public LanguageModel<LanguageEntity> Language { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();


        public DetailsModel(ILanguageService languageService)
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

        public  async Task<IActionResult> OnGet(Guid id)
        {
            var lanModel = await _languageService.GetLanguageAsync<LanguageEntity>(id);
            if (lanModel == null)
            {
                return NotFound();
            }
            Language = lanModel;
            await _languageService.GetLanguageAsync<LanguageEntity>(id);             
            return Page();
        }
    }
}
