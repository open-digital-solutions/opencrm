using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
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
        public string Title { get; set; } = "Accountings";

        [BindProperty]
        public string TablePage { get; set; } = "Accounting";

        [BindProperty]
        public List<string> TableHeaders { get; set; } = new List<string>();

        [BindProperty]
        public List<TableRow<TRowData>> TableRows { get; set; } = new List<TableRow<TRowData>>();

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
                Page = "Accounting",
                Url = "/Finance"
            });
        }

        public void OnGet()
        {
            var acountings = _accountingDataService.GetAccountings();
            if (acountings != null)
            {
                AccountingList = acountings;
                _tableService.BuildTable(AccountingList, TableHeaders, TableRows);
            }
        }
    }
}
