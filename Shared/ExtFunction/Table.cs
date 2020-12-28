using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using NPOI.OpenXml4Net.OPC;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Shared
{
    /// <summary>
    /// System.Data.DataTable 擴充方法
    /// </summary>
    public static partial class ExtFunction
    {
        /// <summary> 
        /// DataTable轉成Excel 
        /// </summary> 
        /// <param name="dt">DataTable</param> 
        /// <param name="workbook">將新的XSSFWorkbook置入</param> 
        /// <param name="path">塞入Excel的路徑</param> 
        /// <param name="FromCol">第幾行開始塞資料</param> 
        public static MemoryStream ToExcel(this DataTable dt, string path, int fromCol)
        {
            OPCPackage pkg = OPCPackage.Open(path, PackageAccess.READ_WRITE);
            XSSFWorkbook workbook = new XSSFWorkbook(pkg);
            ISheet sheet = workbook.GetSheetAt(0);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow(i + fromCol);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row.CreateCell(j);
                    if (dt.Rows[i][j] is DateTime)
                    {
                        cell.SetCellValue(((DateTime)dt.Rows[i][j]).ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    else
                    {
                        cell.SetCellValue(dt.Rows[i][j].ToString());
                    }
                }
            }
            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            return ms;
        }

        public static MemoryStream ToExcel(this DataSet Source, string path, List<int> fromCols)
        {
            OPCPackage pkg = OPCPackage.Open(path, PackageAccess.READ_WRITE);
            XSSFWorkbook workbook = new XSSFWorkbook(pkg);
            for (int k = 0; k < Source.Tables.Count; k++)
            {
                var dt = Source.Tables[k];
                ISheet sheet = workbook.GetSheetAt(k);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet.CreateRow(i + fromCols[k]);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        ICell cell = row.CreateCell(j);
                        if (dt.Rows[i][j] is DateTime)
                        {
                            cell.SetCellValue(((DateTime)dt.Rows[i][j]).ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        else
                        {
                            cell.SetCellValue(dt.Rows[i][j].ToString());
                        }
                    }
                }
            }
            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            return ms;
        }

        /// <summary>
        /// IEnumerable轉成Excel 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="workbook"></param>
        /// <param name="path"></param>
        /// <param name="FromCol"></param>
        public static MemoryStream ToExcel<T>(this IEnumerable<T> list, string path, int FromCol)
        {
            return ToExcel(list.ToDataTable<T>(), path, FromCol);
        }

        /// <summary>
        /// IEnumerable轉成Excel 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static MemoryStream ToExcel<T>(this IEnumerable<T> list)
        {
            return ToExcel(list.ToDataTable<T>());
        }

        public static MemoryStream ToCsv<T>(this IEnumerable<T> list)
        {
            DataTable table = list.ToDataTable<T>();
            return ToCsv(table);
        }

        public static MemoryStream ToExcel(this DataTable SourceTable)
        {
            XSSFWorkbook workbook = new XSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            ISheet sheet = workbook.CreateSheet();
            IRow headerRow = sheet.CreateRow(0);

            // handling header. 
            foreach (DataColumn column in SourceTable.Columns)
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

            // handling value. 
            int rowIndex = 1;

            foreach (DataRow row in SourceTable.Rows)
            {
                IRow dataRow = sheet.CreateRow(rowIndex);
                foreach (DataColumn column in SourceTable.Columns)
                {
                    var cell = dataRow.CreateCell(column.Ordinal);
                    cell.SetCellType(CellType.String);
                    cell.SetCellValue(row[column].ToString());
                }
                rowIndex++;
            }

            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;

            sheet = null;
            headerRow = null;
            workbook = null;

            return ms;
        }

        public static MemoryStream ToCsv(this DataTable table)
        {
            StringBuilder sb = new StringBuilder();
            if (table != null && table.Columns.Count > 0)
            {
                sb.Append(table.Columns.Cast<DataColumn>().Select(r => r.ColumnName).Aggregate((i, j) => i + "," + j)).Append("\n");

                if (table.Rows.Count > 0)
                {
                    foreach (DataRow item in table.Rows)
                    {
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            if (i > 0)
                            {
                                sb.Append(",");
                            }
                            if (item[i] != null)
                            {
                                sb.Append(item[i].ToString());
                            }
                        }
                        sb.Append("\n");
                    }
                }
            }
            MemoryStream stream = new MemoryStream(Encoding.GetEncoding("utf-8").GetBytes(sb.ToString()));
            return stream;
        }

        public static void ExcelToDataTable(this DataTable dt, string path, int sheetAt = 0)
        {
            FileInfo fi = new FileInfo(path);
            if (fi.Extension != ".xls" && fi.Extension != ".xlsx")
            {
                throw new FileLoadException("非Excel檔案", path);
            }
            try
            {
                MemoryStream ms = new MemoryStream();
                using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    file.CopyTo(ms);
                }
                dt.ExcelToDataTable(ms, sheetAt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ExcelToDataTable(this DataTable dt, MemoryStream ms, int sheetAt = 0)
        {
            XSSFWorkbook workbook = new XSSFWorkbook(ms);
            ISheet sheet = workbook.GetSheetAt(sheetAt);
            IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                dt.Columns.Add(column);
            }

            int rowCount = sheet.LastRowNum + 1;

            for (int i = (sheet.FirstRowNum + 1); i < rowCount; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row != null)
                {
                    bool isContinue = false;
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            isContinue = true;
                            break;
                        }
                    }
                    if (!isContinue) break;
                    DataRow dataRow = dt.NewRow();
                    bool isAddToTable = false;
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        //if (row.GetCell(j) != null) 
                        //    dataRow[j] = row.GetCell(j).ToString(); 
                        ICell cell = row.GetCell(j);
                        if (cell == null) continue;
                        if (cell.CellType == CellType.Numeric)
                        {
                            //判斷是否是日期型別
                            if (NPOI.HSSF.UserModel.HSSFDateUtil.IsCellDateFormatted(cell))//日期型別
                            {
                                dataRow[j] = cell.DateCellValue.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            else//其他數字型別 
                            {
                                dataRow[j] = cell.NumericCellValue.ToString();
                            }
                        }
                        else if (cell.CellType == CellType.Blank)//空數據型別
                        {
                            dataRow[j] = "";
                        }
                        else if (cell.CellType == CellType.Formula)//公式型別
                        {
                            XSSFFormulaEvaluator eva = new XSSFFormulaEvaluator(workbook);
                            dataRow[j] = eva.Evaluate(cell).StringValue;
                        }
                        else if (cell.CellType == CellType.Boolean)//Boolean 
                        {
                            dataRow[j] = cell.BooleanCellValue.ToString();
                        }
                        else //其他型別按字串型別來處理
                        {
                            dataRow[j] = cell.StringCellValue;
                        }

                        if (!string.IsNullOrWhiteSpace(((string)dataRow[j])))
                        {
                            isAddToTable = true;
                        }
                    }
                    if (isAddToTable)
                        dt.Rows.Add(dataRow);
                }
            }
            workbook = null;
            sheet = null;
        }

        public static void ExcelToDataSet(this DataSet ds, MemoryStream ms, List<int> sheets = null)
        {
            XSSFWorkbook workbook = new XSSFWorkbook(ms);
            for (int k = 0; k < workbook.NumberOfSheets; k++)
            {
                DataTable dt = new DataTable();
                ISheet sheet = workbook.GetSheetAt(k);

                if (sheets != null && !sheets.Contains(k)) continue;

                IRow headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;

                for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                {
                    DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                    dt.Columns.Add(column);
                }

                int rowCount = sheet.LastRowNum + 1;

                for (int i = (sheet.FirstRowNum + 1); i < rowCount; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row != null)
                    {
                        bool isContinue = false;
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                            {
                                isContinue = true;
                                break;
                            }
                        }
                        if (!isContinue) break;
                        DataRow dataRow = dt.NewRow();
                        bool isAddToTable = false;
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            //if (row.GetCell(j) != null) 
                            //    dataRow[j] = row.GetCell(j).ToString(); 
                            ICell cell = row.GetCell(j);
                            if (cell == null) continue;
                            if (cell.CellType == CellType.Numeric)
                            {
                                //判斷是否是日期型別
                                if (NPOI.HSSF.UserModel.HSSFDateUtil.IsCellDateFormatted(cell))//日期型別
                                {
                                    dataRow[j] = cell.DateCellValue.ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                else//其他數字型別
                                {
                                    dataRow[j] = cell.NumericCellValue.ToString();
                                }
                            }
                            else if (cell.CellType == CellType.Blank)//空數據型別
                            {
                                dataRow[j] = "";
                            }
                            else if (cell.CellType == CellType.Formula)//公式型別
                            {
                                XSSFFormulaEvaluator eva = new XSSFFormulaEvaluator(workbook);
                                dataRow[j] = eva.Evaluate(cell).StringValue;
                            }
                            else if (cell.CellType == CellType.Boolean)//Boolean 
                            {
                                dataRow[j] = cell.BooleanCellValue.ToString();
                            }
                            else //其他型別按字串型別來處理
                            {
                                dataRow[j] = cell.StringCellValue;
                            }

                            if (!string.IsNullOrWhiteSpace(((string)dataRow[j])))
                            {
                                isAddToTable = true;
                            }
                        }
                        if (isAddToTable)
                            dt.Rows.Add(dataRow);
                    }
                }
                sheet = null;
                ds.Tables.Add(dt);
            }
            workbook = null;
        }

        private static string GetDbType(this Type type)
        {
            string result = "VarChar";
            if (type.Equals(typeof(int)) || type.IsEnum)
                result = "Integer";
            else if (type.Equals(typeof(long)))
                result = "Long";
            else if (type.Equals(typeof(double)) || type.Equals(typeof(Double)))
                result = "Double";
            else if (type.Equals(typeof(DateTime)))
                result = "TimeStamp";
            else if (type.Equals(typeof(decimal)))
                result = "Decimal";
            else if (type.Equals(typeof(byte)) || type.Equals(typeof(Byte)))
                result = "Byte";
            return result;
        }

        private static DbType GetOleDbType(this Type type)
        {
            DbType result = DbType.String;
            if (type.Equals(typeof(int)) || type.IsEnum)
                result = DbType.Int32;
            else if (type.Equals(typeof(long)))
                result = DbType.Int64;
            else if (type.Equals(typeof(double)) || type.Equals(typeof(Double)))
                result = DbType.Double;
            else if (type.Equals(typeof(DateTime)))
                result = DbType.DateTime;
            else if (type.Equals(typeof(bool)))
                result = DbType.Boolean;
            else if (type.Equals(typeof(string)))
                result = DbType.String;
            else if (type.Equals(typeof(decimal)))
                result = DbType.Decimal;
            else if (type.Equals(typeof(byte[])))
                result = DbType.Binary;
            else if (type.Equals(typeof(Guid)))
                result = DbType.Guid;
            else if (type.Equals(typeof(byte)) || type.Equals(typeof(Byte)))
                result = DbType.Byte;
            return result;
        }

        public static void WriteContextToTrace(this DataTable dt, List<string> columnNames, bool isHtml)
        {
            if (isHtml)
            {
                int width = 1350;
                StringBuilder context = new StringBuilder();
                context.Append(string.Format("</br><table border=1 cellpadding=2 cellspacing=0 width={0} align=center>", width.ToString()));
                if (columnNames != null)
                {
                    context.Append(string.Format("<tr bgcolor=gray><td colspan={0} align=center><b><font color=yellow>{1}</font></td></tr>", columnNames.Count, dt.TableName));
                    context.Append("<tr>");
                    foreach (string colName in columnNames)
                    {
                        context.Append(string.Format("<td><font color=navy size=2>{0}</font></td>", colName));
                    }
                    context.Append("</tr>");
                    foreach (DataRow row in dt.Rows)
                    {
                        context.Append("<tr>");
                        foreach (string colname in columnNames)
                        {
                            context.Append("<td><font color=navy size=2>" + row[colname].ToString() + "</font></td>");
                        }
                        context.Append("</tr>");
                    }
                }
                else
                {
                    context.Append(string.Format("<tr bgcolor=gray><td colspan={0} align=center><b><font color=yellow>{1}</font></td></tr>", dt.Columns.Count, dt.TableName));
                    context.Append("<tr>");
                    foreach (DataColumn col in dt.Columns)
                    {
                        context.Append(string.Format("<td><font color=navy size=2>{0}</font></td>", col.ColumnName));
                    }
                    context.Append("</tr>");
                    foreach (DataRow row in dt.Rows)
                    {
                        context.Append("<tr>");
                        foreach (DataColumn col in dt.Columns)
                        {
                            context.Append("<td><font color=navy size=2>" + row[col.ColumnName].ToString() + "</font></td>");
                        }
                        context.Append("</tr>");
                    }
                }
                context.Append("</table>");
                Trace.WriteLine(context.ToString());
            }
            else
            {
                string msg = "";
                string format = "";

                if (columnNames != null)
                {
                    for (int i = 0; i < columnNames.Count; i++)
                    {
                        string str = "{" + i + "}\t";
                        format += str;

                        msg += string.Format("{0}\t", columnNames[i].Trim().PadRight(20, ' '));
                    }
                    Trace.TraceWarning(msg);


                    foreach (DataRow row in dt.Rows)
                    {
                        List<string> rowData = new List<string>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            if (columnNames.Contains(col.ColumnName))
                            {
                                rowData.Add(row[col.ColumnName].ToString().Trim().PadRight(20, ' '));
                            }
                        }
                        Trace.TraceWarning(format, rowData.ToArray());
                    }
                }
                else
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string str = "{" + i + "}\t";
                        format += str;

                        msg += string.Format("{0}\t", dt.Columns[i].ColumnName.Trim().PadRight(20, ' '));
                    }
                    Trace.TraceWarning(msg);


                    foreach (DataRow row in dt.Rows)
                    {
                        List<string> rowData = new List<string>();
                        foreach (DataColumn col in dt.Columns)
                        {
                            rowData.Add(row[col.ColumnName].ToString().Trim().PadRight(20, ' '));
                        }
                        Trace.TraceWarning(format, rowData.ToArray());
                    }
                }
            }
        }

        /// <summary>   
        /// 將 IEnumerable 轉換成 DataTable   
        /// </summary>   
        /// <param name="list">IEnumerable</param>   
        /// <returns>DataTable</returns>   
        public static DataTable ToDataTable(this IEnumerable list)
        {
            PropertyInfo[] props1 = list.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

            DataTable dt2 = (DataTable)props1[2].GetValue(list, null);
            return dt2;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null || varlist.Count() == 0)
            {
                PropertyInfo[] pInfo = typeof(T).GetProperties();
                foreach (PropertyInfo pi in pInfo)
                {
                    if (pi.PropertyType.IsClass && !(pi.PropertyType == typeof(string) || pi.PropertyType == typeof(byte[])))
                        continue; 
                    Type colType = pi.PropertyType;

                    if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                    {
                        colType = colType.GetGenericArguments()[0];
                    }

                    dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                }
                return dtReturn;
            }

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow 
                if (oProps == null)
                {
                    oProps = rec.GetType().GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        if (pi.PropertyType.IsClass && !(pi.PropertyType == typeof(string) || pi.PropertyType == typeof(byte[]))) 
                            continue;
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    if (pi.PropertyType.IsClass && !(pi.PropertyType == typeof(string) || pi.PropertyType == typeof(byte[])))
                        continue;
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
    }
}