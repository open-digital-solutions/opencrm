using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace OpenDHS.Shared.Data
{
    public class DataBlockEntity  : BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Type { get; set; }

        [Column(TypeName = "jsonb")]
        public required string Data { get; set; }


        //Relations
        public Guid? DataContanerId { get; set; } // Required foreign key property
        public DataContainerEntity? DataContainer { get; set; } = null!; // Required reference navigation to principal
    }
}
