using System.ComponentModel.DataAnnotations;

namespace OpenCRM.Core.Web.Models
{
    public class TextBlockModel
    {
        [Required]
        public string Code { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

    }
}
