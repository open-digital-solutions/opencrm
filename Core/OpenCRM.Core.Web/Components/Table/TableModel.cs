using Microsoft.AspNetCore.Mvc;
using OpenCRM.Core.Web.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Components.Table
{
    public class TableModel
    {
        private readonly string _title;
        private readonly string _page;

        public TableModel(string title, string tablePage) 
        { 
            _title = title;
            _page = tablePage;
        }

        public string Title {
            get
            {
                return _title;
            }
        }

        public string TablePage { 
            get
            {
                return _page;
            } 
        }

        public List<string> TableHeaders { get; set; } = new List<string>();

        public List<TableRow<TRowData>> TableRows { get; set; } = new List<TableRow<TRowData>>();
    }
}
