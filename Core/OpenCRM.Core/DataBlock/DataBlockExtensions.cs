using OpenDHS.Shared.Data;
using System.Text.Json;

namespace OpenCRM.Core.DataBlock
{
    public static class DataBlockExtensions
    {
        public static DataBlockModel<TDataModel>? ToDataModel<TDataModel>(this DataBlockEntity entity) {

            if (entity == null || string.IsNullOrWhiteSpace(entity.Data))
            {
                //TODO: Handle this error
                return null;
            }

            var data = JsonSerializer.Deserialize<TDataModel>(entity.Data);

            //TODO: Handle this error
            if (data == null) return null;

            return new DataBlockModel<TDataModel>
            {
                ID = entity.ID,
                Name = entity.Name,
                Description = entity.Description,
                Type = entity.Type,
                Data = data,
                AddedAt = entity.AddedAt,
                UpdatedAt = entity.UpdatedAt,
                DeletedAt = entity.DeletedAt,
            };
        }

    }
}
