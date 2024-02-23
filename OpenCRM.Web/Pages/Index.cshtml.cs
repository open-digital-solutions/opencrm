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
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var blockModel = new CardBlockModel
            {
                Code = "KEY_BLOCKCARD_DEMO",
                Title = "Galaxy",
                SubTitle = "Puple Galaxy in Universe",
                Type = BlockType.Card,
                Description = "A galaxy is a collection of gases, dust and billions of stars and their solar systems. The galaxy is held together by the force of gravity.",
                ImageUrl = "http://localhost:5005/media/21e7b1d0-1008-495d-825a-11cd5b417b2d.jpg"
            };

            var dataBlockModel = new DataBlockModel<CardBlockModel>
            {
                Code = blockModel.Code,
                Description = blockModel.Title,
                Data = blockModel,
                Type = BlockType.Card.ToString()
            };

            var block  = await _blockService.GetBlockByCode(blockModel.Code) ?? await _blockService.AddBlock(dataBlockModel);
            
            if(block != null)
            {
                Block = block.Data;
            }

            var dataSesison = _identityService.GetSession();
            if (dataSesison == null) Lang = "IT";
            Lang = dataSesison != null ? dataSesison.Lang : "Default dal browser";

            return Page();
        }
    }
}
