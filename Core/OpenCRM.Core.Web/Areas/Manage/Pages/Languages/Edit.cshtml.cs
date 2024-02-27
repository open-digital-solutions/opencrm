using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.LanguageService;
using System.Text.Json;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Languages
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ILanguageService _languageService;

        [BindProperty]
        public LanguageModel Language { get; set; } = default!;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public EditModel(ILanguageService languageService)
        {
            _languageService = languageService;

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "Manage",
                IsActive = true,
                Name = "Languages",
                Page = "Languages",
                Url = "/Manage/Languages"
            });
        }

        public async Task<IActionResult> OnGet(Guid id)
        {
            var languageModel = await _languageService.GetLanguage(id);

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
                var languageModelEdit = new LanguageModel()
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