using OpenCRM.Core.DataBlock;
using OpenCRM.Core.Web.Components.Table;
using OpenCRM.Core.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Table
{
    public class TableService<TDataModel>
	{
		public TableService() { }

        /// <summary>
        /// Create a button type element
        /// </summary>
        /// <param name="row">Row where the button will be created</param>
        /// <param name="label">Button name</param>
        /// <param name="iconName">Button icon</param>
        private void BuildButton(TableRow<TRowData> row, string label, string iconName)
		{
			row.Datas.Add(new TRowData()
			{
				Label = label,
				IsButton = true,
				IconName = iconName,
			});
		}

        public bool IsImage(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            return extension == ".png" || extension == ".jpg" || extension == ".jpeg" ||
                    extension == ".gif" || extension == ".svg" || extension == ".webp";
        }
        
        private TRowData CreateRowData(TDataModel data, string prop)
        {
            var propValue = data?.GetType().GetProperty(prop)?.GetValue(data)?.ToString();

            if (!string.IsNullOrEmpty(propValue) && IsImage(propValue))
            {
                TRowData rowData = new TRowData()
                {
                    Label = propValue,
                    IsImage = true,
                    ImageUrl = propValue
                };

                return rowData;
            }
            else
            {
                TRowData rowData = new TRowData()
                {
                    Label = propValue
                };

                return rowData;
            }
        }

        /// <summary>
        /// Create a table
        /// </summary>
        /// <param name="datas">Data used to create the table</param>
		/// <returns>A tuple where the first element is the table headers and the second element is the table rows</returns>
        public Tuple<List<string>, List<TableRow<TRowData>>> BuildTable(List<DataBlockModel<TDataModel>> datas, string NameEntity ="")
		{
			var properties = typeof(TDataModel).GetProperties();
			List<string> tableHeaders = new List<string>();
			List< TableRow <TRowData>> tableRows = new List<TableRow<TRowData>>();

            foreach (var prop in properties)
			{

				if ((prop.Name.Equals("ID") || prop.Name.Equals("Translations")) && NameEntity.Equals("Language"))
					continue;

                  tableHeaders.Add(prop.Name);
            }

            foreach (var item in datas)
			{
				TableRow<TRowData> row = new TableRow<TRowData>();
				row.ID = item.ID;
				
				foreach (var prop in tableHeaders)
				{
                    if ((prop.Equals("ID") || prop.Equals("Translations")) && NameEntity.Equals("Language"))
                        continue;


                    var data = item.Data;
					var propValue = data?.GetType().GetProperty(prop)?.GetValue(data)?.ToString();

                    //if(propValue )

                    //TRowData rowData = new TRowData()
                    //{
                    //	Label = propValue
                    //};
                    //row.Datas.Add(rowData);
                }

                BuildButton(row, "Edit", "fas fa-pen");
				BuildButton(row, "Details", "fas fa-info-circle");
				BuildButton(row, "Delete", "fas fa-trash");

				tableRows.Add(row);
			}

			return new Tuple<List<string>, List<TableRow<TRowData>>>(tableHeaders, tableRows);
		}
	}
}
