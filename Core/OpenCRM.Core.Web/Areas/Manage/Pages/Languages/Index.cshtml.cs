using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Data;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Components.Table;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.LanguageService;
using OpenCRM.Core.Web.Table;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Languages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILanguageService _languageService;
        
        private TableService<LanguageModel> _tableService;

        [BindProperty]
        public List<LanguageModel> LanguageList { get; set; } = new List<LanguageModel>();

        [BindProperty]
        public TableModel Table { get; set; } = new TableModel("Languages", "Languages");

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public IndexModel(ILanguageService languageService)
        { 
            _languageService = languageService;
            _tableService = new TableService<LanguageModel>();

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "Manage",
                IsActive = true,
                Name = "Languages",
                Page = "Languages",
                Url = "/Manage"
            });            
        }

        public void OnGet()
        {            
            var result = _languageService.GetLanguageListAsync();

            if (result != null)
            {
                var response = result.Select(f => new DataBlockModel<LanguageModel> { Data = f, ID = f.ID, Description = f.Name, Code = f.Code, Type = "" }).ToList();

                var tableResult = _tableService.BuildTable(response, "Language");
                Table.Headers = tableResult.Item1;
                Table.Rows = tableResult.Item2;
            }
        }
    }
}