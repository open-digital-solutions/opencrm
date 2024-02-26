using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Components.Table;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services;
using OpenCRM.Core.Web.Table;

namespace OpenCRM.Core.Web.Areas.Manage.Pages.Media
{
    public class IndexModel : PageModel
    {
        private readonly IMediaService _mediaService;
        private TableService<MediaTableModel> _tableService { get; set; } = new TableService<MediaTableModel>();

        [BindProperty]
        public List<MediaEntity> MediaList { get; set; } = new List<MediaEntity>();
        [BindProperty]
        public List<BreadCrumbLinkModel> Links { get; set; } = new List<BreadCrumbLinkModel>();

        [BindProperty]
        public TableModel Table { get; set; } = new TableModel("Medias", "Media");

        public IndexModel(IMediaService mediaService)
        {
            _mediaService = mediaService;
            _tableService = new TableService<MediaTableModel>();

            Links.Add(new BreadCrumbLinkModel
            {
                Area = "",
                IsActive = true,
                Name = "Home...",
                Page = "",
                Url = "/"
            });
            Links.Add(new BreadCrumbLinkModel
            {
                Area = "Manage",
                IsActive = true,
                Name = "Media List",
                Page = "Media",
                Url = "/Manage"
            });

        }

        public void OnGet()
        {
            var result = _mediaService.GetMedias();
            var response = result.Select(f => new DataBlockModel<MediaTableModel> { Data = new MediaTableModel{Name=f.FileName.ToString(), Type=f.FileType.ToString(),IsPublic=f.IsPublic,UpdatedAt=f.UpdatedAt}, Description =f.FileType.ToString(), Code = f.FileName.ToString(), Type = "", ID = f.ID }).ToList();
            
            var tableResult = _tableService.BuildTable(response);
            Table.Headers = tableResult.Item1;
            Table.Rows = tableResult.Item2;
        }
    }
}
