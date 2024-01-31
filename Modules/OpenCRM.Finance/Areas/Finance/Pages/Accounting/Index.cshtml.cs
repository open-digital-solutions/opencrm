using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Components.Table;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Table;
using OpenCRM.Finance.Services;

namespace OpenCRM.Finance.Areas.Finance.Pages.Accounting
{
    public class IndexModel : PageModel
    {
        private readonly IAccountingService _accountingDataService;

        private TableService<AccountingModel> _tableService { get; set; } = new TableService<AccountingModel>();

        [BindProperty]
        public List<DataBlockModel<AccountingModel>> AccountingList { get; set; } = new List<DataBlockModel<AccountingModel>>();

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        [BindProperty]
        public TableModel Table { get; set; } = new TableModel("Accountings", "Accounting");

        public IndexModel(IAccountingService accountingDataService)
        {
            _accountingDataService = accountingDataService;

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
                Area = "Finance",
                IsActive = true,
                Name = "Accounting List",
                Page = "",
                Url = "/Finance"
            });
        }

        public void OnGet()
        {
            var acountings = _accountingDataService.GetAccountings();
            if (acountings != null)
            {
                AccountingList = acountings;
                
                var result = _tableService.BuildTable(AccountingList);
                Table.Headers = result.Item1;
                Table.Rows = result.Item2;
            }
        }
    }
}
