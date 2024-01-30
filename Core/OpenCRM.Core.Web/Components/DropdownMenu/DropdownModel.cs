using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Components.DropdownMenu
{
    public class DropdownModel
    {
        private string _name;

        private string _url;

        private List<DropdownModel> _items;


        public DropdownModel(string name = "", string url = "")
        {
            _name = name;
            _url = url;
            _items = new List<DropdownModel>();
        }

        public DropdownModel(string name, string url, List<DropdownModel> items)
        {
            _name = name;
            _url = url;
            _items = items;
        }

        public string Name { get { return _name; } set { _name = value; } }

        public string Url { get { return _url; } set { _url = value; } }

        public List<DropdownModel> Items { get { return _items; } set { _items = value; } }

    }
}
