using OpenCRM.Core.Data;
using System.ComponentModel.DataAnnotations;


namespace OpenCRM.Core
{
    public class MediaEntity : BaseEntity
    {
        public bool IsPublic { get; set; }
        public string? FileName { get; set; }
        public byte[]? FileData { get; set; }
        public MediaType FileType { get; set; }
    }
}
