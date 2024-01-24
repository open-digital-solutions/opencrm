using Microsoft.AspNetCore.Http;

namespace OpenCRM.Core
{
    public class MediaUploadModel
    {
        public IFormFile? FileDetails { get; set; }
        public bool IsPublic { get; set; } = false;
    }
}
