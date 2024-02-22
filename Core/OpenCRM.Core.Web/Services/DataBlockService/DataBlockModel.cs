using OpenCRM.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.DataBlock
{
    public class DataBlockModel<TDataModel> 
    {
        public Guid ID { get; set; }
        public required string Code { get; set; }
        public required string Description { get; set; }
        public required string Type { get; set; }
        public required TDataModel Data { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime DeletedAt { get; set; } = DateTime.UtcNow;
    }
}
