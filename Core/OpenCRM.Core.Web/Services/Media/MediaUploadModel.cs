using Microsoft.AspNetCore.Http;

namespace OpenCRM.Core.Web.Services
{
    public class MediaUploadModel
    {
        public IFormFile? FileDetails { get; set; }
        public bool IsPublic { get; set; } = false;
    }
}
