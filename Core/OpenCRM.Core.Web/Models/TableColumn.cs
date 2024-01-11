using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Models
{
    //public interface ITableColumnData {
        
    //}
    public class TableColumn //<TColumnData> where TColumnData : ITableColumnData
    {
        public string Header { get; set; } = "Header";
        public string Label { get; set; } = "Label";
        public bool IsButton { get; set; } = false;
        // public TColumnData? Data { get; set; }
    }
}
