using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Models.BlockModels;

namespace OpenCRM.Core.Web.Services.BlockServices.TextBlockService
{
	public interface ITextBlockService
    {
		TextBlockModel CreateBlockModel(string code, string description);
		Task<DataBlockModel<TextBlockModel>?> AddBlock(DataBlockModel<TextBlockModel> model);
        Task<DataBlockModel<TextBlockModel>?> EditBlock(DataBlockModel<TextBlockModel> model);
        Task RemoveBlock(Guid Id);
        Task<DataBlockModel<TextBlockModel>?> GetBlock(Guid Id);
        Task<DataBlockModel<TextBlockModel>?> GetBlockByCode(string code);
        Task<List<DataBlockModel<TextBlockModel>>> GetTextBlocks();
        Task Seed();
    }
}
