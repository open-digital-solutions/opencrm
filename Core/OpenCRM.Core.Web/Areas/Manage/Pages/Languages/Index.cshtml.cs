using Microsoft.AspNetCore.Mvc;
using OpenCRM.Core.Data;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Components.Table;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.LanguageService;
using OpenCRM.Core.Web.Table;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Languages
{
    public class IndexModel : CorePageModel
    {
        private readonly ILanguageService _languageService;
        
        private TableService<LanguageModel<LanguageEntity>> _tableService { get; set; } = new TableService<LanguageModel<LanguageEntity>>();

        [BindProperty]
        public List<LanguageModel<LanguageEntity>> LanguageList { get; set; } = new List<LanguageModel<LanguageEntity>>();

        [BindProperty]
        public TableModel Table { get; set; } = new TableModel("Languages", "Languages");

        public IndexModel(ILanguageService languageService)
        { 
        
            _languageService = languageService;

            var link = new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Home",
                Page = "",
                Url = "/"
            };

            Links.Add(link);

            var link2 = new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Languages",
                Page = "",
                Url = "/Manage/Languages"
            };

            Links.Add(link2);

           /* var link3 = new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Translations",
                Page = "",
                Url = "/Manage/Translations/Index"
            };

            Links.Add(link3);*/
        }

        public  void OnGet()
        {
            
            var result = _languageService.GetLanguageListAsync<LanguageEntity>();
            var response = result.Select(f => new DataBlockModel<LanguageModel<LanguageEntity>> { Data = f, ID = f.ID , Description = f.Name, Name = f.Code , Type = "" }).ToList();
            
            var tableResult = _tableService.BuildTable(response);
            Table.Headers = tableResult.Item1;
            Table.Rows = tableResult.Item2;
        }
    }
}
