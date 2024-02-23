using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Components.DropdownMenu
{
    public class DropdownMenuModel
    {
        private string _name;
        private string _url;
        private bool _showItemList;
        private List<DropdownMenuModel> _items;

        public DropdownMenuModel(string name = "", string url = "")
        {
            _name = name;
            _url = url;
            _showItemList = false;
            _items = new List<DropdownMenuModel>();
        }

        public DropdownMenuModel(string name, string url, List<DropdownMenuModel> items, bool showItemList = true)
        {
            _name = name;
            _url = url;
            _items = items;
            _showItemList = showItemList;
        }

        public string Name { get { return _name; } set { _name = value; } }

        public string Url { get { return _url; } set { _url = value; } }

        public List<DropdownMenuModel> Items { get { return _items; } set { _items = value; } }

        public bool ShowItemList { get { return _showItemList; } set { _showItemList = value; } }

        public DropdownMenuModel? FindItemByUrl(string url)
        {
            var result = (Items.Count() != 0)? Items.Find(item => item.Url == url) : null;
            return result;
        }
    }
}
