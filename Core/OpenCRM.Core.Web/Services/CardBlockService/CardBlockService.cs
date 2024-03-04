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

        public async Task<CardBlockModel?> EditBlock(CardBlockModel model)
        {
            var cardBlock = await _dataBlockService.GetDataBlockByCode<CardBlockModel>(model.Code);
            if (cardBlock == null) return null;
            cardBlock.Data = model;
            var updatedCardBlock = await _dataBlockService.EditBlock(cardBlock);
            return updatedCardBlock?.Data;
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
        public CardBlockModel CreateBlockModel(string code, string description)
        {
            var blockModel = new CardBlockModel()
            {
                Code = code,
                Description = description,
                Type = BlockType.Text
            };

            return blockModel;
        }

        public async Task<DataBlockModel<CardBlockModel>?> GetBlockByCode(string code)
        {
            var result = await _dataBlockService.GetDataBlockByCode<CardBlockModel>(code);
            return result;
        }

        public async Task Seed()
        {
            var blockModel = new CardBlockModel
            {
                Code = "KEY_BLOCKCARD_DEMO",
                Type = BlockType.Text,
                Description = "A galaxy is a collection of gases, dust and billions of stars and their solar systems. The galaxy is held together by the force of gravity.",
            };

            var dataBlockModel = new DataBlockModel<CardBlockModel>
            {
                Code = blockModel.Code,
                Description = blockModel.Code,
                Data = blockModel,
                Type = BlockType.Text.ToString()
            };

            var block = await GetBlockByCode(blockModel.Code) ?? await AddBlock(dataBlockModel);
        }
    }
}
