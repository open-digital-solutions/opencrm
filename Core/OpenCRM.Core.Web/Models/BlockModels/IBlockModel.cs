using OpenCRM.Core.Web.Models.BlockModels;
using System.ComponentModel.DataAnnotations;

namespace OpenCRM.Core.Web.Models
{
	public enum BlockType
	{
		None,
		Text,
		Card
	}

	public interface IBlockModel
	{
		[Required]
		string Code { get; set; }

		[Required]
		BlockType Type { get; set; }
	}
}
