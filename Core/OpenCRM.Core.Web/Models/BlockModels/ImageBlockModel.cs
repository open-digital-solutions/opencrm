using OpenCRM.Core.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Models.BlockModels
{
	public class ImageBlockModel : IBlockModel
	{
		public string Code { get; set; } = string.Empty;

		public int? With { get; set; }

		public int? Height { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public BlockType Type { get; set; } = BlockType.Image;
	}
}
