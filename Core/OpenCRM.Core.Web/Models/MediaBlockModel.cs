using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Models
{
    public class MediaBlockModel
    {
        public Guid Id { get; set; }

        public string ImageName { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;
    }
}
