﻿using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models.BlockModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Services.BlockService
{
    public interface IBlockService
	{
		Task<DataBlockModel<IBlockModel>?> AddBlock(DataBlockModel<IBlockModel> model);

		Task<DataBlockModel<IBlockModel>?> EditBlock(DataBlockModel<IBlockModel> model);

		Task<DataBlockModel<IBlockModel>?> GetBlock(Guid Id);

		Task<List<DataBlockModel<IBlockModel>>> GetBlocks();
		Task RemoveBlock(Guid Id);

		Task Seed();
	}
}
