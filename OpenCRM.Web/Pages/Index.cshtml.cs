using Microsoft.AspNetCore.Mvc;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.CardBlockService;
using OpenCRM.Core.Web.Services.IdentityService;
using OpenCRM.Core.Web.Services.TranslationService;

namespace OpenCRM.Web.Pages
{
    public class IndexModel : CorePageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly ICardBlockService _blockService;
        private readonly IIdentityService _identityService;
        private readonly ITranslationService _translationService;

        [BindProperty]
        public CardBlockModel? Block { get; set; }

        [BindProperty]
        public Dictionary<string, string>? Translations { get; set; } = new Dictionary<string, string>();

        public string? Lang { get; set; }
        public IndexModel(ILogger<IndexModel> logger, ICardBlockService blockService, IIdentityService identityService, ITranslationService translationService)
        {
            _logger = logger;
            _blockService = blockService;
            _identityService = identityService;
            _translationService = translationService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            //var block = await _blockService.Seed();

            //if (block != null)
            //{
            //    Block = block.Data;
            //}

            Translations = new Dictionary<string, string>();
           
            var keyMainLabel = await _translationService.GetTranslationValueAsync("KEY_MANAGE_WELCOME");
            Translations.Add("KEY_MANAGE_WELCOME", keyMainLabel ?? "KEY_MANAGE_WELCOME");

            return Page();
        }
    }
}
