using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Services.CardBlockService
{
    public interface ICardBlockService
    {
        Task<DataBlockModel<CardBlockModel>?> AddBlock(DataBlockModel<CardBlockModel> model);
        CardBlockModel CreateBlockModel(string code, string title, string? subTitle, string? description, string? imageId);
        Task<DataBlockModel<CardBlockModel>?> EditBlock(DataBlockModel<CardBlockModel> model);
        Task<DataBlockModel<CardBlockModel>?> GetBlock(Guid Id);
        Task<List<DataBlockModel<CardBlockModel>>> GetBlocks();
        Task<DataBlockModel<CardBlockModel>?> ShowCardBlock();
        Task RemoveBlock(Guid Id);
    }
}