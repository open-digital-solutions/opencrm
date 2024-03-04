using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Services.CardBlockService
{
    public interface ICardBlockService
    {
        Task<DataBlockModel<CardBlockModel>?> AddBlock(DataBlockModel<CardBlockModel> model);
        CardBlockModel CreateBlockModel(string code, string description);
        Task<CardBlockModel?> EditBlock(CardBlockModel model);
        Task<DataBlockModel<CardBlockModel>?> EditBlock(DataBlockModel<CardBlockModel> model);
        Task<DataBlockModel<CardBlockModel>?> GetBlock(Guid Id);
        Task<DataBlockModel<CardBlockModel>?> GetBlockByCode(string code);
        Task<List<DataBlockModel<CardBlockModel>>> GetBlocks();
        Task RemoveBlock(Guid Id);
        Task Seed();
    }
}