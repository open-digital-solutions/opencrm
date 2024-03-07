using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Models.BlockModels;

namespace OpenCRM.Core.Web.Services.ImageBlockService
{
	public interface IImageBlockService
	{
		ImageBlockModel? CreateBlockModel(string code, string? imageId, int? with, int? height);
		Task<DataBlockModel<ImageBlockModel>?> AddBlock(DataBlockModel<ImageBlockModel> model);
		Task<DataBlockModel<ImageBlockModel>?> EditBlock(DataBlockModel<ImageBlockModel> model);
		Task RemoveBlock(Guid Id);
		Task<DataBlockModel<ImageBlockModel>?> GetBlock(Guid Id);
		Task<DataBlockModel<ImageBlockModel>?> GetBlockByCode(string code);
		Task<List<DataBlockModel<ImageBlockModel>>> GetImageBlocks();
		Task Seed();
	}
}
