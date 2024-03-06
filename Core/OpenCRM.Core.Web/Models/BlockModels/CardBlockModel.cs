using System.ComponentModel.DataAnnotations;
using OpenCRM.Core.Web.Models.BlockModels;

namespace OpenCRM.Core.Web.Models
{
    public class CardBlockModel : IBlockModel
    {
        [Required] 
        public string Code { get; set; } = string.Empty;

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? SubTitle { get; set; }

        public BlockType Type { get; set; } = BlockType.Card;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }
    }
}
