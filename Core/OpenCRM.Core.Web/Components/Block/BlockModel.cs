using OpenCRM.Core.Web.Components.Block.Description;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Components.Block
{
    public enum BlockType
    {
        Text,
        Card
    }

    public class BlockModel
    {
		public string Title { get ; set; } = string.Empty;
		
		public string? SubTitle { get; set; }

		public BlockType Type { get; set; } = BlockType.Text;

		public DescriptionModel? Description { get; set; }
        public string? Image { get; set; }
    }
}
