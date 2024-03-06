using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Models.BlockModels;

namespace OpenCRM.Core.Web.Services.BlockServices.TextBlockService
{
    public class TextBlockService : ITextBlockService
    {
        IDataBlockService _dataBlockService;

        public TextBlockService(IDataBlockService dataBlockService)
        {
            _dataBlockService = dataBlockService;
        }

        public async Task<DataBlockModel<TextBlockModel>?> AddBlock(DataBlockModel<TextBlockModel> model)
        {
            var blocks = await GetTextBlocks();

            foreach (var block in blocks)
            {
                if (block.Data.Code == model.Data.Code)
                {
                    return null;
                }
            }
            return await _dataBlockService.AddBlock(model);
        }

        public async Task<DataBlockModel<TextBlockModel>?> EditBlock(DataBlockModel<TextBlockModel> model)
        {
            return await _dataBlockService.EditBlock(model);
        }

        public async Task<DataBlockModel<TextBlockModel>?> GetBlock(Guid Id)
        {
            return await _dataBlockService.GetDataBlockAsync<TextBlockModel>(Id);
        }

        public async Task<DataBlockModel<TextBlockModel>?> GetBlockByCode(string code)
        {
            return await _dataBlockService.GetDataBlockByCode<TextBlockModel>(code);
        }

        public async Task<List<DataBlockModel<TextBlockModel>>> GetTextBlocks()
        {
            return await _dataBlockService.GetDataBlockListAsync<TextBlockModel>();
        }

        public async Task RemoveBlock(Guid Id)
        {
            await _dataBlockService.DeleteBlock<CardBlockModel>(Id);
        }

        public async Task Seed()
        {
            var block = await GetBlockByCode("KEY_BLOCKTEXT_DEMO");

            if (block == null)
            {
                var textBlock = new TextBlockModel
                {
                    Code = "KEY_BLOCKTEXT_DEMO",
                    Description = "Starbucks is the best coffee chain in the world.",
                };

                var dataBlock = new DataBlockModel<TextBlockModel>
                {
                    Code = textBlock.Code,
                    Description = textBlock.Code,
                    Data = textBlock,
                    Type = BlockType.Card.ToString()
                };

                await AddBlock(dataBlock);
            }
        }
    }
}
