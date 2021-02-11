using OfficeOpenXml;
using System.Data;
using System.IO;

namespace CoreApp.Common.Helpers
{
    public static class ExcelHelper
    {
        /// <summary>
        /// Read data from excel file and return data table
        /// </summary>
        /// <param name="file">FileInfo</param>
        /// <returns>Data table</returns>
        public static DataTable ReadData(FileInfo file)
        {
            var resultTable = new DataTable();
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                int rowCount = worksheet.Dimension.Rows;
                int ColCount = worksheet.Dimension.Columns;
                bool bHeaderRow = true;

                // Write Header Row
                foreach (var firstRowCell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
                {
                    resultTable.Columns.Add(bHeaderRow ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                }

                // Write Body
                var startBodyRow = bHeaderRow ? 2 : 1;
                for (int rowNum = startBodyRow; rowNum <= worksheet.Dimension.End.Row; rowNum++)
                {
                    var wsRow = worksheet.Cells[rowNum, 1, rowNum, /*worksheet.Dimension.End.Column*/9];
                    DataRow row = resultTable.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                }

                return Distinct(resultTable);
            }
        }

        /// <summary>
        /// Distinct data table
        /// </summary>
        /// <param name="dataTable">Data table</param>
        /// <returns>Data table</returns>
        private static DataTable Distinct(DataTable dataTable)
        {
            var arrColumn = new string[dataTable.Columns.Count];
            for (var i = 0; i < dataTable.Columns.Count; i++)
            {
                arrColumn[i] = dataTable.Columns[i].ColumnName;
            }
            dataTable = dataTable.DefaultView.ToTable(true, arrColumn);

            return dataTable;
        }
    }
}
