using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Services.CardBlockService
{
    public class CardBlockService : ICardBlockService
    {
        public readonly IDataBlockService _dataBlockService;
        private readonly IMediaService _mediaService;

        public CardBlockService(IDataBlockService dataBlockService, IMediaService mediaService)
        {
            _dataBlockService = dataBlockService;
            _mediaService = mediaService;
        }


        //TODO: Trabajar directamente con el CardBlockModel
        public async Task<DataBlockModel<CardBlockModel>?> AddBlock(DataBlockModel<CardBlockModel> model)
        {
            var blocks = await GetBlocks();

            foreach (var block in blocks)
            {
                if (block.Data.Code == model.Data.Code)
                {
                    return null;
                }
            }
            return await _dataBlockService.AddBlock(model);
        }

        //TODO: Trabajar directamente con el CardBlockModel

        public async Task<DataBlockModel<CardBlockModel>?> EditBlock(DataBlockModel<CardBlockModel> model)
        {
            return await _dataBlockService.EditBlock(model);
        }

        public async Task<DataBlockModel<CardBlockModel>?> GetBlock(Guid Id)
        {
            return await _dataBlockService.GetDataBlockAsync<CardBlockModel>(Id);
        }
        //TODO: Trabajar directamente con el CardBlockModel
        public async Task<List<DataBlockModel<CardBlockModel>>> GetBlocks()
        {
            var result = await _dataBlockService.GetDataBlockListAsync<CardBlockModel>();
            return result;
        }
        //TODO: Trabajar directamente con el CardBlockModel

        public async Task RemoveBlock(Guid Id)
        {
            await _dataBlockService.DeleteBlock<CardBlockModel>(Id);
        }
        //TODO: Trabajar directamente con el CardBlockModel
        public CardBlockModel CreateBlockModel(string code, string title, string? subTitle, string? description, string? imageId)
        {
            if (!string.IsNullOrEmpty(imageId))
            {
                var imageGuiId = Guid.Parse(imageId);
                var images = _mediaService.GetImageMedias();
                var image = images.Find(img => img.Id == imageGuiId);

                var blockModel = new CardBlockModel()
                {
                    Code = code,
                    Title = title,
                    SubTitle = subTitle,
                    Description = description,
                    Type = BlockType.Card,
                    ImageUrl = image?.ImageUrl,
                };

                return blockModel;
            }
            else
            {
                var blockModel = new CardBlockModel()
                {
                    Code = code,
                    Title = title,
                    Description = description,
                    SubTitle = subTitle,
                    Type = BlockType.Text
                };

                return blockModel;
            }
        }
    }
}
