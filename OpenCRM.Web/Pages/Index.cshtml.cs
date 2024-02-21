using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.IdentityService;
using OpenCRM.Core.Web.Services.BlockService;

namespace OpenCRM.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IBlockService _blockService;
        private readonly IIdentityService _identityService;

        private readonly IBlockService _blockService;

        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        [BindProperty]
        public BlockModel Block { get; set; } = new BlockModel();

        public string? Lang { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IBlockService blockService, IIdentityService identityService)
        {
            _logger = logger;
            _blockService = blockService;

            _identityService = identityService;
            _blockService = blockService;

            var link = new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Home",
                Page = ""
            };

            Links.Add(link);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            string id = "24c5d1e0-dc43-4dee-8790-cbf6d495e7f1";
            var dataBlockModel = await _blockService.GetBlock(Guid.Parse(id));

            if (dataBlockModel == null)
            {
                return NotFound();
            }

            var blockModel = new BlockModel
            {
                Code = dataBlockModel.Data.Code,
                Title = dataBlockModel.Data.Title,
                SubTitle = dataBlockModel.Data.SubTitle,
                Type = dataBlockModel.Data.Type,
                Description = dataBlockModel.Data.Description,
                ImageUrl = dataBlockModel.Data.ImageUrl,
            };

            Block = blockModel;

            return Page();            var dataSesison = _identityService.GetSession();
            if (dataSesison == null) Lang = "IT";
            Lang = dataSesison != null ? dataSesison.Lang : "Default dal browser";
        }
    }
}