using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Models.BlockModel
{
    public enum BlockType
    {
        Text,
        Card
    }

    public class BlockDescription
    {
        public string? Text { get; set; }

        public List<BlockDescription>? ItemsList { get; set; }
    }

    public class BlockTextModel : IBlockModel
    {
        public Guid Id { get; set; }

        public BlockType Type { get; set; } = BlockType.Text;

        [Required]
        public string MainText { get; set; } = string.Empty;

        public string? SubText { get; set; }

        public BlockDescription? Description { get; set; }

    }

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
