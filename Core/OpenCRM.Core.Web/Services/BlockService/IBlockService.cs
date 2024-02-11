using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Components.Block;
using OpenCRM.Core.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Services.BlockService
{
    public interface IBlockService
	{
		Task<DataBlockModel<BlockModel>?> AddBlock(DataBlockModel<BlockModel> model);

		Task<DataBlockModel<BlockModel>?> EditBlock(DataBlockModel<BlockModel> model);

		Task<DataBlockModel<BlockModel>?> GetBlock(Guid Id);

		Task<List<DataBlockModel<BlockModel>>> GetBlocks();
		Task RemoveBlock(Guid Id);

		Task Seed();
	}
}
