using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OpenCRM.Core
{
    public class MediaModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Is Public")]
        public bool IsPublic { get; set; }
        [Display(Name = "File Name")]
        public string FileName { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        [Required(ErrorMessage = "You must selected a file.")]
        [Display(Name = "File")]
        public required IFormFile FileData { get; set; }

    }

    public class MediaTableModel
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool IsPublic { get; set; }
        public DateTime UpdatedAt { get; set; } 
       }
}
