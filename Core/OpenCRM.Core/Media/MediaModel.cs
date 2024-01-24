using Microsoft.AspNetCore.Http;

namespace OpenCRM.Core
{
    public class MediaModel
    {
        public Guid Id { get; set; }
        public bool IsPublic { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
    
    }
}
