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

		private readonly IMediaService _mediaService;

		public BlockService(IDataBlockService dataBlockService, IMediaService mediaService)
		{
			_dataBlockService = dataBlockService;
			_mediaService = mediaService;
		}

		public async Task<DataBlockModel<BlockModel>?> AddBlock(DataBlockModel<BlockModel> model)
		{
			var blocks = await GetBlocks();

			foreach(var block in blocks)
			{
				if(block.Data.Code == model.Data.Code)
				{
					return null;
				}
			}
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

        public BlockModel CreateBlockModel(string code, string title, string? subTitle, string? description, string? imageId)
        {
            if (!string.IsNullOrEmpty(imageId))
            {
                var imageGuiId = Guid.Parse(imageId);
                var images = _mediaService.GetImageMedias();
                var image = images.Find(img => img.Id == imageGuiId);

                var blockModel = new BlockModel()
                {
                    Code = code,
                    Title = title,
                    SubTitle = subTitle,
                    Type = BlockType.Card,
                    ImageId = imageGuiId,
                    ImageUrl = image?.ImageUrl,
                };

                return blockModel;
            }
            else
            {
                var blockModel = new BlockModel()
                {
                    Code = code,
                    Title = title,
                    SubTitle = subTitle,
                    Type = BlockType.Text
                };

                return blockModel;
            }
        }
    }
}
