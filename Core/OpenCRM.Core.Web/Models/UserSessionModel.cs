using OpenCRM.Core.Web.Services.LanguageService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Models
{
    public class UserSessionModel
    {
        public required string UserId { get; set; }
        public required string LanguageId { get; set; }
        public required string CypherKey { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime IssuedDate { get; set; }    
    }
}
