using OpenCRM.Core.DataBlock;
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

		private void BuildButton(TableRow<TRowData> row, string label, string iconName)
		{
			row.Datas.Add(new TRowData()
			{
				Label = label,
				IsButton = true,
				IconName = iconName,
			});
		}

		public void BuildTable(List<DataBlockModel<TDataModel>> datas, List<string> tableHeaders, List<TableRow<TRowData>> tableRows)
		{
			var properties = typeof(TDataModel).GetProperties();

			foreach (var prop in properties)
				tableHeaders.Add(prop.Name);

			foreach (var item in datas)
			{
				TableRow<TRowData> row = new TableRow<TRowData>();
				row.ID = item.ID;

				foreach (var prop in tableHeaders)
				{
					var data = item.Data;
					var propValue = data?.GetType().GetProperty(prop)?.GetValue(data)?.ToString();

					if (propValue != null)
					{
						TRowData rowData = new TRowData()
						{
							Label = propValue
						};
						row.Datas.Add(rowData);
					}
				}

				BuildButton(row, "Edit", "fas fa-pen");
				BuildButton(row, "Details", "fas fa-info-circle");
				BuildButton(row, "Delete", "fas fa-trash");

				tableRows.Add(row);
			}
		}
	}
}
