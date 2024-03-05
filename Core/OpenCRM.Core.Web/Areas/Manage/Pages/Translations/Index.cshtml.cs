using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Components.Table;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.TranslationService;
using OpenCRM.Core.Web.Table;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Translations
{
	public class IndexModel : PageModel
    {
        private readonly ITranslationService _translationService;

        private TableService<TranslationLanguageCodeModel> _tableService;

        [BindProperty]
        public List<TranslationModel> TranslationList { get; set; } = new List<TranslationModel>();

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        [BindProperty]
        public TableModel Table { get; set; } = new TableModel("Translations", "Translations");

        public IndexModel(ITranslationService translationService)
        {
            _translationService = translationService;
            _tableService = new TableService<TranslationLanguageCodeModel>();

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "Manage",
                IsActive = true,
                Name = "Translations",
                Page = "Translations",
                Url = "/Manage/Translations"
            });
        }

        public void OnGet()
        {
            var result = _translationService.GetTranslationListAsync();

            if (result != null)
            {
                var response = _translationService.ToListDataBlockModel(result);
                var tableResult = _tableService.BuildTable(response, "Translation");
                Table.Headers = tableResult.Item1;
                Table.Rows = tableResult.Item2;
            }
        }
    }
}
