using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Services.BlockService
{
    public class BlockService<TDBContext> : IBlockService where TDBContext : DataContext
	{
		public readonly IDataBlockService _dataBlockService;

		public BlockService(IDataBlockService dataBlockService)
		{
			_dataBlockService = dataBlockService;
		}

		public async Task<DataBlockModel<BlockModel>?> AddBlock(DataBlockModel<BlockModel> model)
		{
			return await _dataBlockService.AddBlock(model);
		}

		public async Task<DataBlockModel<BlockModel>?> EditBlock(DataBlockModel<BlockModel> model)
		{
			return await _dataBlockService.EditBlock(model);
		}

		public async Task<DataBlockModel<BlockModel>?> GetBlock(Guid Id)
		{
			return await _dataBlockService.GetDataBlockAsync<BlockModel>(Id);
		}

		public async Task<List<DataBlockModel<BlockModel>>> GetBlocks()
		{
			var result = await _dataBlockService.GetDataBlockListAsync<BlockModel>();
			return result;
		}

		public async Task RemoveBlock(Guid Id)
		{
			await _dataBlockService.DeleteBlock<BlockModel>(Id);
		}

		public Task Seed()
		{
			throw new NotImplementedException();
		}
	}
}
