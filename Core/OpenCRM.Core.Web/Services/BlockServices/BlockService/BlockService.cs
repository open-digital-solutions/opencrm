﻿using Microsoft.EntityFrameworkCore;
using OpenCRM.Core.Data;
using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Models.BlockModels;
using System.Data;
using System.Text.Json;

namespace OpenCRM.Core.Web.Services.BlockServices.BlockService
{
    public class BlockService<TDBContext> : IBlockService where TDBContext : DataContext
    {
        private readonly TDBContext _dbContext;

        public BlockService(TDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public bool CheckBlockType(string dbType)
        {
            return dbType == typeof(TextBlockModel).Name || dbType == typeof(CardBlockModel).Name;
        }

        public IBlockModel? GetBlockByType (string type, DataBlockEntity dataBlock)
        {
            switch(type)
            {
                case "TextBlockModel":
                    return JsonSerializer.Deserialize<TextBlockModel>(dataBlock.Data);

                case "CardBlockModel":
                    return JsonSerializer.Deserialize<CardBlockModel>(dataBlock.Data);

                case "ImageBlockModel":
                    return JsonSerializer.Deserialize<ImageBlockModel>(dataBlock.Data);
			}
            return null;
		}

		private DataBlockModel<BlockModel>? GetDataBlockInstance(string type, DataBlockEntity dataBlock)
        {
			IBlockModel? block = GetBlockByType(type, dataBlock);

			if (block != null)
            {
                var data = new BlockModel()
                {
                    Code = block.Code,
                    Type = block.Type,
                    Block = dataBlock.Data
				};

                var dataModel = new DataBlockModel<BlockModel>()
                {
                    Code = block.Code,
                    Description = block.Code,
                    Type = typeof(BlockModel).Name,
                    Data = data
                };

                return dataModel;
            }

            return null;
        }

        public async Task<List<DataBlockModel<BlockModel>>?> GetBlocks()
        {
            try
            {
                var result = new List<DataBlockModel<BlockModel>>();

                var dataBlocks = await _dbContext.DataBlocks.Where(block => (block.Type == typeof(TextBlockModel).Name || block.Type == typeof(CardBlockModel).Name) || block.Type == typeof(ImageBlockModel).Name).ToListAsync();

                if (dataBlocks == null || dataBlocks.Count == 0) return result;

                foreach (var dataBlock in dataBlocks)
                {
                    if (dataBlock != null && !string.IsNullOrWhiteSpace(dataBlock.Data))
                    {
                        var dataModel = GetDataBlockInstance(dataBlock.Type, dataBlock);

                        if (dataModel != null)
                        {
                            result.Add(dataModel);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}