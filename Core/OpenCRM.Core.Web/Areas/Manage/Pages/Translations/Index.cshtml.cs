using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Data;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Components.Table;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.TranslationService;
using OpenCRM.Core.Web.Table;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Translations
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ITranslationService _translationService;

        private TableService<TranslationModel<TranslationEntity>> _tableService;

        [BindProperty]
        public List<TranslationModel<TranslationEntity>> TranslationList { get; set; } = new List<TranslationModel<TranslationEntity>>();

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        [BindProperty]
        public TableModel Table { get; set; } = new TableModel("Translations", "Translations");

        public IndexModel(ITranslationService translationService)
        {

            _translationService = translationService;
            _tableService = new TableService<TranslationModel<TranslationEntity>>();

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Home ...",
                Page = "",
                Url = "/"
            });

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "/Manage",
                IsActive = true,
                Name = "Translation List",
                Page = "Translations",
                Url = "/Manage"
            });
        }

        public void OnGet()
        {
            var result = _translationService.GetTranslationListAsync<TranslationEntity>();
            var response = result.Select(f => new DataBlockModel<TranslationModel<TranslationEntity>> { Data = f, Description = f.Translation, Code = f.Key, Type = "", ID=f.ID}).ToList();

            var tableResult = _tableService.BuildTable(response);
            Table.Headers = tableResult.Item1;
            Table.Rows = tableResult.Item2;
        }
    }
}
