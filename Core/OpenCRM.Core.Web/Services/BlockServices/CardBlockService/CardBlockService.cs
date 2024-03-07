using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Models.BlockModels;

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

        public async Task<DataBlockModel<CardBlockModel>?> AddBlock(DataBlockModel<CardBlockModel> model)
        {
            var blocks = await GetCardBlocks();

            foreach (var block in blocks)
            {
                if (block.Data.Code == model.Data.Code)
                {
                    return null;
                }
            }
            return await _dataBlockService.AddBlock(model);
        }

        public async Task<CardBlockModel?> EditBlock(CardBlockModel model)
        {
            var cardBlock = await _dataBlockService.GetDataBlockByCode<CardBlockModel>(model.Code);
            if (cardBlock == null) return null;
            cardBlock.Data = model;
            var updatedCardBlock = await _dataBlockService.EditBlock(cardBlock);
            return updatedCardBlock?.Data;
        }

        public async Task<DataBlockModel<CardBlockModel>?> EditBlock(DataBlockModel<CardBlockModel> model)
        {
            return await _dataBlockService.EditBlock(model);
        }

        public async Task<DataBlockModel<CardBlockModel>?> GetBlock(Guid Id)
        {
            return await _dataBlockService.GetDataBlockAsync<CardBlockModel>(Id);
        }

        public async Task<List<DataBlockModel<CardBlockModel>>> GetCardBlocks()
        {
            var result = await _dataBlockService.GetDataBlockListAsync<CardBlockModel>();
            return result;
        }

        public async Task RemoveBlock(Guid Id)
        {
            await _dataBlockService.DeleteBlock<CardBlockModel>(Id);
        }

        public CardBlockModel? CreateBlockModel(string code, string title, string? subTitle, string? description, string? imageId)
        {
            if (!string.IsNullOrEmpty(imageId))
            {
                var imageGuiId = Guid.Parse(imageId);
                var images = _mediaService.GetImageMedias();
                var image = images.Find(img => img.Id == imageGuiId);

                if (image != null)
                {
                    var cardBlock = new CardBlockModel()
                    {
                        Code = code,
                        Title = title,
                        SubTitle = subTitle,
                        Description = description,
                        Type = BlockType.Card,
                        ImageUrl = image?.ImageUrl,
                    };

                    return cardBlock;
                }
            }
            return null;   
        }

        public async Task<DataBlockModel<CardBlockModel>?> GetBlockByCode(string code)
        {
            var result = await _dataBlockService.GetDataBlockByCode<CardBlockModel>(code);
            return result;
        }

        public async Task<DataBlockModel<CardBlockModel>?> ShowCardBlock()
        {
            var blockModel = new CardBlockModel
            {
                Code = "KEY_BLOCKCARD_DEMO",
                Title = "Galaxy",
                SubTitle = "Puple Galaxy in Universe",
                Type = BlockType.Card,
                Description = "A galaxy is a collection of gases, dust and billions of stars and their solar systems. The galaxy is held together by the force of gravity.",
                ImageUrl = "http://localhost:5005/media/1c2b4ba4-d49b-4c56-9fc1-c258041f75a8.jpg"
            };

            var dataBlockModel = new DataBlockModel<CardBlockModel>
            {
                Code = blockModel.Code,
                Description = blockModel.Title,
                Data = blockModel,
                Type = BlockType.Card.ToString()
            };

            var block = await GetBlockByCode(blockModel.Code) ?? await AddBlock(dataBlockModel);
            return block;
        }
    }
}
