using Microsoft.AspNetCore.Mvc;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.CardBlockService;
using OpenCRM.Core.Web.Services.IdentityService;

namespace OpenCRM.Web.Pages
{
    public class IndexModel : CorePageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly ICardBlockService _blockService;
        private readonly IIdentityService _identityService;

        [BindProperty]
        public CardBlockModel Block { get; set; } = new CardBlockModel();

        public string? Lang { get; set; }
        public IndexModel(ILogger<IndexModel> logger, ICardBlockService blockService, IIdentityService identityService)
        {
            _logger = logger;
            _blockService = blockService;
            _identityService = identityService;

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
            //string id = "24c5d1e0-dc43-4dee-8790-cbf6d495e7f1";
            //var dataBlockModel = await _blockService.

            var blockModel = new CardBlockModel
            {
                Code = "KEY_BLOCKCARD_DEMO",
                Title = "Title",
                SubTitle = "Sub Title",
                Type = BlockType.Card,
                Description = "Description",
                ImageUrl = "http://localhost:5005/media/02d40f99-619f-4dea-b640-6a44b5898eca.png"
            };
            var dataBlockModel = new DataBlockModel<CardBlockModel> { Code = "sdcsdcsd", Description = "sdcsdcsdcsdc", Data = blockModel, Type = BlockType.Card.ToString() };



            var createdDataBLock = await _blockService.AddBlock(dataBlockModel);
            if (createdDataBLock != null) {
                Block = createdDataBLock.Data;
            }

            var dataSesison = _identityService.GetSession();
            if (dataSesison == null) Lang = "IT";
            Lang = dataSesison != null ? dataSesison.Lang : "Default dal browser";

            return Page();
        }
    }
}
