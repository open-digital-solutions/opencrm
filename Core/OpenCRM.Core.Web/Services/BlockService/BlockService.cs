﻿using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models.BlockModel;
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

		public async Task<DataBlockModel<IBlockModel>?> AddBlock(DataBlockModel<IBlockModel> model)
		{
			return await _dataBlockService.AddBlock(model);
		}

		public async Task<DataBlockModel<IBlockModel>?> EditBlock(DataBlockModel<IBlockModel> model)
		{
			return await _dataBlockService.EditBlock(model);
		}

		public async Task<DataBlockModel<IBlockModel>?> GetBlock(Guid Id)
		{
			return await _dataBlockService.GetDataBlockAsync<IBlockModel>(Id);
		}

		public async Task<List<DataBlockModel<IBlockModel>>> GetBlocks()
		{
			var result = await _dataBlockService.GetDataBlockListAsync<IBlockModel>();
			if (result == null) return new List<DataBlockModel<IBlockModel>>();
			return result;
		}

		public async Task RemoveBlock(Guid Id)
		{
			await _dataBlockService.DeleteBlock<IBlockModel>(Id);
		}

		public Task Seed()
		{
			throw new NotImplementedException();
		}
	}
}
