using Microsoft.AspNetCore.Mvc;
using OpenCRM.Core.Web.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Components.Table
{
    public class TRowData
    {
        public string Label { get; set; } = string.Empty;

        public bool IsButton { get; set; } = false;

        public string? Url { get; set; }

        public string? IconName { get; set; }

    }

    public class TableRow<TRowData>
    {
        public Guid ID { get; set; }

        public List<TRowData> Datas { get; set; } = new List<TRowData>();
    }

    public class TableModel
    {
        private string _title;
        private string _page;
        private List<string> _headers;
        private List<TableRow<TRowData>> _rows;

        public TableModel(string title="", string tablePage="") 
        { 
            _title = title;
            _page = tablePage;
            _headers = new List<string>();
            _rows = new List<TableRow<TRowData>>();

        }

        public TableModel(string title, string tablePage, List<string> headers, List<TableRow<TRowData>> rows)
        {
            _title = title;
            _page = tablePage;
            _headers = headers;
            _rows = rows;
        }

        public string Title { get { return _title; } set { _title = value; } }

        public string Page { get { return _page; } set {  _page = value; } }

        public List<string> Headers { get { return _headers; } set { _headers = value; } }

        public List<TableRow<TRowData>> Rows { get { return _rows; } set { _rows = value; } }
    }
}
