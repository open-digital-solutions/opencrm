using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using String = System.Runtime.InteropServices.JavaScript.JSType.String;

namespace OpenCRM.Core.Web.Models
{
    public class TranslationModel
    {
        [Required]
        [RegularExpression(@"^\{\s*""KeyAccept"":\s*""(.*?)""\s*,\s*""\}$")]
        public string KeyAccept { get; set; } = "Accept";

        [Required]
        [RegularExpression(@"^\{\s*""KeyCreate"":\s*""(.*?)""\s*,\s*""\}$")]
        public string KeyCreate { get; set; } = "Create";

        public override string ToString()
        {
            return "{"+ System.String.Format("KeyAccept:{0} , KeyCreate: {1}", KeyAccept, KeyCreate) + "}" ;
        }
    }
}
