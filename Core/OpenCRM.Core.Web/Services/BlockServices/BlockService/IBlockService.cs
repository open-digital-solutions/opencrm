using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models.BlockModels;

namespace OpenCRM.Core.Web.Services.BlockServices.BlockService
{
    public interface IBlockService
    {
        Task<List<DataBlockModel<BlockModel>>?> GetBlocks();
    }
}
