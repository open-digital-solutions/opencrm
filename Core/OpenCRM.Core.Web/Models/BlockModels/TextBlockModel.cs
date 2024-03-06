using System.ComponentModel.DataAnnotations;
using OpenCRM.Core.Web.Models.BlockModels;

namespace OpenCRM.Core.Web.Models
{
    public class TextBlockModel : IBlockModel
    {
        [Required]
        public string Code { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public BlockType Type { get; set; } = BlockType.Text;

    }
}
