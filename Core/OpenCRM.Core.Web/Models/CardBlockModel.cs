using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Models
{
    public enum BlockType{
        Text,
        Card
    }

    public class CardBlockModel
    {
        [Required] 
        public string Code { get; set; } = string.Empty;

        public string? Title { get; set; }

        public string? SubTitle { get; set; }

        public BlockType Type { get; set; } = BlockType.Card;

        public string Description { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }
    }
}
