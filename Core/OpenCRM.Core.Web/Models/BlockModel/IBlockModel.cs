using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Models.BlockModel
{
	public interface IBlockModel
	{
		Guid Id { get; set; }

		BlockType Type { get; set; }

		[Required]
		string MainText { get; set; }

		string? SubText { get; set; }

		BlockDescription? Description { get; set; }
	}
}
