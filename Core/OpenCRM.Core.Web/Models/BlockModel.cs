using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Models
{
    public enum BlockType{
        Text,
        Card
    }

    public class BlockModel
    {

        public string Title { get; set; } = string.Empty;

        public string? SubTitle { get; set; }

        public BlockType Type { get; set; } = BlockType.Text;

        public string? Description { get; set; }

        public Guid? ImageId { get; set; }

        public string? ImageUrl { get; set; }
    }
}
