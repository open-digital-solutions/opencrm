using Microsoft.AspNetCore.Http;

namespace OpenDHS.Shared
{
    public class MediaUploadModel
    {
        public IFormFile? FileDetails { get; set; }
        public bool IsPublic { get; set; } = false;
    }
}
