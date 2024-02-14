using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Graph.CallRecords;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services;
using OpenCRM.Core.Web.Services.BlockService;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.DataBlock
{
    public class CreateModel : PageModel
    {
        private readonly IBlockService _blockService;

        private readonly IMediaService _mediaService;

        public string ValidateError { get; set; } = string.Empty;

        [BindProperty]
        public BlockModel Model { get; set; } = default!;


        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        public CreateModel(IBlockService blockService, IMediaService mediaService)
        {
            _blockService = blockService;
            _mediaService = mediaService;

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Home",
                Page = "",
                Url = "/"
            });

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "",
                IsActive = true,
                Name = "Manage",
                Page = "",
                Url = "/Manage/Index"
            });

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "Manage",
                IsActive = true,
                Name = "Block",
                Page = "",
                Url = "/Manage/Block"
            });

            Links.Add(new BreadCrumbLinkModel()
            {
                Area = "Manage",
                IsActive = true,
                Name = "Create Block",
                Page = "",
                Url = "/Manage/Block/Create"
            });
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public List<DescriptionItem> ToListItem()
        {
            var list = new List<DescriptionItem>();
            return list;
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var modelType = BlockType.Text;
              
                var blockModel = new BlockModel()
                {
                    Code = Model.Code,
                    Title = Model.Title,
                    SubTitle = Model.SubTitle,
                    Type = modelType,
                    //ImageId = imageID,
                    //ImageUrl = imageUrl
                };

                var medias = _mediaService.GetImageMedias();

                var dataBlockModel = new DataBlockModel<BlockModel>()
                {
                    Name = blockModel.Title,
                    Description = blockModel.Title,
                    Type = typeof(BlockModel).Name,
                    Data = blockModel
                };

                if (await _blockService.AddBlock(dataBlockModel) == null)
                {
                    ValidateError = "Block with code " + Model.Code + " already exists";
                    return Page();
                }
                
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
