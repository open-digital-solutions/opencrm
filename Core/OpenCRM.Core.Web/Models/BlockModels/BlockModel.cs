using System.ComponentModel.DataAnnotations;

namespace OpenCRM.Core.Web.Models.BlockModels
{
	public class BlockModel : IBlockModel
    {
        [Required]
        public string Code { get; set; } = string.Empty;

        [Required]
        public BlockType Type { get; set; } = BlockType.None;

        [Required]
        public string Block { get; set; } = "{}";
    }
}
