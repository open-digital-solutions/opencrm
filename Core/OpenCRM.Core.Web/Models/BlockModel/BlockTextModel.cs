﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Models.BlockModel
{
	public class BlockTextModel : IBlockModel
	{
		public Guid Id { get; set; }

		public BlockType Type { get; set; } = BlockType.Text;

		[Required]
		public string MainText { get; set; } = string.Empty;

		public string? SubText { get; set; }

		public BlockDescription? Description { get; set; }

	}
}
