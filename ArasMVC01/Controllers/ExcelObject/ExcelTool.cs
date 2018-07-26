using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClosedXML.Excel;
using Newtonsoft.Json;
namespace ArasMVC01.Controllers.ExcelObject
{
    public class ExcelTool
    {
        public ExcelTool()
        {

        }
        public object  ExcelToJson(string excel_path,string isHeader,string importStartRow)
        {
            List<Dictionary<string,object>> resultJson = new List<Dictionary<string,object>>();
            
            List<string> header = new List<string>();

            XLWorkbook workbook = new XLWorkbook(excel_path);
            IXLWorksheet worksheet = workbook.Worksheets.Worksheet(1);

            var firstCell = worksheet.FirstCellUsed();
            var lastCell = worksheet.LastCellUsed();
            var firstRow = worksheet.FirstRow();
            
            int hearderColumnsCount = lastCell.Address.ColumnNumber;
            if (isHeader == "checked")
            {
                for (int col = 1; col <= hearderColumnsCount; col++)
                {
                    header.Add(worksheet.Cell(1, col).Value.ToString());
                }
            }
            else
            {
                Dictionary<string, object> jo = new Dictionary<string, object>();
                
                for (int col = 1; col <= hearderColumnsCount; col++)
                {
                    string id = "col_" + col.ToString();
                    header.Add(id);
                    jo.Add(id, id);
                }
                resultJson.Add(jo);
            }
            int row = 1;
            foreach(IXLRow dr in worksheet.Rows())
            {
                if(row != 1)
                {
                    if (int.Parse(importStartRow) > row)
                    {
                        row += 1;
                        continue;
                    }
                }
               
                Dictionary<string, object> jo = new Dictionary<string, object>();
                jo.Add("sort_order", row);
                var i = 1;
                foreach(var head in header)
                {
                    jo.Add(head, dr.Cell(i).Value.ToString());
                    i += 1;
                }
                resultJson.Add(jo);
                row += 1;
            }
            return resultJson;
        }
    }
}