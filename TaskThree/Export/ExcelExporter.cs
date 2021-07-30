using System.Collections.Generic;
using System.Reflection;
using TaskThree.Models;
using Excel = Microsoft.Office.Interop.Excel;

namespace TaskThree.Export
{
    class ExcelExporter: IExporter
    {
        public void Export(List<Record> records, string fileName) 
        {
            Excel.Application excel = new Excel.Application();
            excel.DisplayAlerts = true;
            Excel.Workbook workbook = excel.Workbooks.Add(Missing.Value);
            Excel.Worksheet sheet = (Excel.Worksheet)workbook.Sheets[1];
            sheet.Columns.ColumnWidth = 20;
            sheet.Name = "Records";

            string[] fields = { "id", "Date", "FirstName", "LastName", "SurName", "City", "Country" };

            for (int i = 0; i < fields.Length; i++)
                ((Excel.Range)sheet.Cells[1, i + 1]).Value2 = fields[i];

            for(int i = 0; i < records.Count; i++)
            {
                ((Excel.Range)sheet.Cells[i + 2, 1]).Value2 = records[i].Id;
                ((Excel.Range)sheet.Cells[i + 2, 2]).Value2 = records[i].Date.ToString("dd.MM.yyyy");
                ((Excel.Range)sheet.Cells[i + 2, 3]).Value2 = records[i].FirstName;
                ((Excel.Range)sheet.Cells[i + 2, 4]).Value2 = records[i].LastName;
                ((Excel.Range)sheet.Cells[i + 2, 5]).Value2 = records[i].SurName;
                ((Excel.Range)sheet.Cells[i + 2, 6]).Value2 = records[i].City;
                ((Excel.Range)sheet.Cells[i + 2, 7]).Value2 = records[i].Country;
            }
            workbook.SaveAs(fileName ?? "records.xlsx");
            excel.Quit();
        }
    }
}
