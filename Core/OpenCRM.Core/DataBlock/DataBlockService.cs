using OpenDHS.Shared;
using OpenDHS.Shared.Data;
using System.Text.Json;

namespace OpenCRM.Core.DataBlock
{
    public class DataBlockService<TDBContext> : IDataBlockService where TDBContext : DataContext
    {
        private readonly TDBContext _dbContext;
        public DataBlockService(TDBContext dBContext) { 
            _dbContext = dBContext;
        }
        public async Task<DataBlockModel<TDataModel>?> GetDataBlockAsync<TDataModel>(Guid id) { 
           
            try {
                var dataBlock = await _dbContext.DataBlocks.FindAsync(id);
                if (dataBlock == null || string.IsNullOrWhiteSpace(dataBlock.Data))
                {
                    //TODO: Handle this error
                    return null;
                }
                return dataBlock.ToDataModel<TDataModel>();
            }
            catch (Exception ex) {
                //TODO: Handle this error
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        public List<DataBlockModel<TDataModel>> GetDataBlockListAsync<TDataModel>()
        {
            List<DataBlockModel<TDataModel>> result = new();
            try
            {
                var dataBlocks = _dbContext.DataBlocks.Where(block => block.Type == typeof(TDataModel).Name).ToList();

                if (dataBlocks == null || dataBlocks.Count == 0) return result;

                foreach ( var dataBlock in dataBlocks)
                {
                    if (dataBlock == null || string.IsNullOrWhiteSpace(dataBlock.Data))
                    {
                        //TODO: Handle this error
                        continue;
                    }
                    var data = JsonSerializer.Deserialize<TDataModel>(dataBlock.Data);

                    //TODO: Handle this error
                    if (data == null) continue;

                    var dataModel = dataBlock.ToDataModel<TDataModel>();

                    if (dataModel == null) continue;

                    result.Add(dataModel);
                }
            }
            catch (Exception ex)
            {
                //TODO: Handle this error
                Console.WriteLine(ex.ToString());
                return new List<DataBlockModel<TDataModel>>();
            }
            return result;
        }
        public async Task<DataBlockModel<TDataModel>?> AddBlock<TDataModel>(DataBlockModel<TDataModel> model) {
            try {
                //TODO: Handle errors and exceptions
                var entity = Activator.CreateInstance<DataBlockEntity>();
                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.Type = typeof(TDataModel).Name;
                entity.Data = JsonSerializer.Serialize(model.Data);
                _dbContext.DataBlocks.Add(entity);
                await _dbContext.SaveChangesAsync();
                return entity.ToDataModel<TDataModel>();
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return null;
            } 
        }
        public async Task DeleteBlock<TDataModel>(Guid Id) {
            //TODO: Handle errors and exceptions
            var entity = await _dbContext.DataBlocks.FindAsync(Id);
            if (entity == null) return;
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<DataBlockModel<TDataModel>?> EditBlock<TDataModel>(DataBlockModel<TDataModel> model) {
            try
            {
                //TODO: Handle errors and exceptions
                var entity = await _dbContext.DataBlocks.FindAsync(model.ID);
                if (entity == null) return null;
                if(entity.Type != typeof(TDataModel).Name) return null;
                entity.Name = model.Name;
                entity.Description = model.Description;
                entity.Type = typeof(TDataModel).Name;
                entity.Data = JsonSerializer.Serialize(model.Data);
                _dbContext.DataBlocks.Update(entity);
                await _dbContext.SaveChangesAsync();
                return entity.ToDataModel<TDataModel>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
