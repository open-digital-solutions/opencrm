using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Services.BlockServices.TextBlockService
{
    public interface ITextBlockService
    {
        Task<DataBlockModel<TextBlockModel>?> AddBlock(DataBlockModel<TextBlockModel> model);
        Task<DataBlockModel<TextBlockModel>?> EditBlock(DataBlockModel<TextBlockModel> model);
        Task RemoveBlock(Guid Id);
        Task<DataBlockModel<TextBlockModel>?> GetBlock(Guid Id);
        Task<DataBlockModel<TextBlockModel>?> GetBlockByCode(string code);
        Task<List<DataBlockModel<TextBlockModel>>> GetTextBlocks();
        Task Seed();
    }
}
