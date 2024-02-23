using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenCRM.Core.Data;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Components.Table;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.LanguageService;
using OpenCRM.Core.Web.Table;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Languages
{
    [Authorize]
    public class IndexModel : CorePageModel
    {
        private readonly ILanguageService _languageService;
        
        private TableService<LanguageModel<TranslationModel>> _tableService;

        [BindProperty]
        public List<LanguageModel<TranslationModel>> LanguageList { get; set; } = new List<LanguageModel<TranslationModel>>();

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        [BindProperty]
        public TableModel Table { get; set; } = new TableModel("Languages", "Languages");

        public IndexModel(ILanguageService languageService)
        { 
        
            _languageService = languageService;
            _tableService = new TableService<LanguageModel<TranslationModel>>();

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
                Area = "Manage",
                IsActive = true,
                Name = "Languages List",
                Page = "Languages",
                Url = "/Manage"
            });            
        }

        public void OnGet()
        {            
            var result = _languageService.GetLanguageListAsync<TranslationModel>();
            var response = result.Select(f => new DataBlockModel<LanguageModel<TranslationModel>> { Data = f, ID = f.ID , Description = f.Name, Code = f.Code , Type = "" }).ToList();
            
            var tableResult = _tableService.BuildTable(response, "Language");
            Table.Headers = tableResult.Item1;
            Table.Rows = tableResult.Item2;
        }
    }
}