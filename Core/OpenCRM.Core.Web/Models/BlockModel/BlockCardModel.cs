using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Models.BlockModel
{
	public class BlockCardModel : IBlockModel
	{
		public Guid Id { get; set; }

		public BlockType Type { get; set; } = BlockType.Card;

		[Required]
		public string MainText { get; set; } = string.Empty;

		public string? SubText { get; set; }

		public BlockDescription? Description { get; set; }

		public string Image = string.Empty;
	}
}
