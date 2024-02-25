using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using String = System.Runtime.InteropServices.JavaScript.JSType.String;

namespace OpenCRM.Core.Web.Models
{
    public class TranslationModel
    {

        public Dictionary<string, string> Translations { get; set; } = new Dictionary<string, string>();

        public override string ToString()
        {
            return JsonSerializer.Serialize<Dictionary<string, string>>(Translations);
        }
    }
}