using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Models.BlockModels;

namespace OpenCRM.Core.Web.Services.ImageBlockService
{
    internal class ImageBlockService : IImageBlockService
	{
		private IDataBlockService _dataBlockService;

        private readonly IMediaService _mediaService;


        public ImageBlockService(IDataBlockService dataBlockService, IMediaService mediaService)
        {
            _dataBlockService = dataBlockService;
            _mediaService = mediaService;
        }

        public ImageBlockModel? CreateBlockModel(string code, string? imageId, int? with, int? height)
		{
			if (!string.IsNullOrEmpty(imageId))
			{
				var imageGuiId = Guid.Parse(imageId);
				var images = _mediaService.GetImageMedias();
				var image = images.Find(img => img.Id == imageGuiId);

				if (image != null)
				{
					var imageBlock = new ImageBlockModel()
					{
						Code = code,
						With = with ?? 400,
						Height = height,
						ImageUrl = image.ImageUrl
					};

					return imageBlock;
				}
			}
            return null;
        }

        public async Task<DataBlockModel<ImageBlockModel>?> AddBlock(DataBlockModel<ImageBlockModel> model)
		{
			var blocks = await GetImageBlocks();

			foreach (var block in blocks)
			{
				if (block.Data.Code == model.Data.Code)
				{
					return null;
				}
			}
			return await _dataBlockService.AddBlock(model);
		}

		public async Task<DataBlockModel<ImageBlockModel>?> EditBlock(DataBlockModel<ImageBlockModel> model)
		{
			return await _dataBlockService.EditBlock(model);
		}

		public async Task<DataBlockModel<ImageBlockModel>?> GetBlock(Guid Id)
		{
			return await _dataBlockService.GetDataBlockAsync<ImageBlockModel>(Id);
		}

		public async Task<DataBlockModel<ImageBlockModel>?> GetBlockByCode(string code)
		{
			return await _dataBlockService.GetDataBlockByCode<ImageBlockModel>(code);
		}

		public async Task<List<DataBlockModel<ImageBlockModel>>> GetImageBlocks()
		{
			return await _dataBlockService.GetDataBlockListAsync<ImageBlockModel>();
		}

		public async Task RemoveBlock(Guid Id)
		{
			await _dataBlockService.DeleteBlock<CardBlockModel>(Id);
		}

		public async Task Seed()
		{
            var block = await GetBlockByCode("KEY_BLOCKIMAGE_DEMO");

			if (block == null)
			{
				var imageBlock = new ImageBlockModel()
				{
					Code = "KEY_BLOCKIMAGE_DEMO",
					With = 400,
					Height = null,
					ImageUrl = "http://localhost:5005/media/1c2b4ba4-d49b-4c56-9fc1-c258041f75a8.jpg"
                };

				var dataBlock = new DataBlockModel<ImageBlockModel>()
				{
					Code = imageBlock.Code,
					Description = imageBlock.Code,
					Type = typeof(ImageBlockModel).Name,
					Data = imageBlock
				};

				await AddBlock(dataBlock);
			}
		}
	}
}
