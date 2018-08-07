
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Web;

namespace DotNet.Common
{
    public static class DataTableExtensions
    {
       public static string GetValue(this DataRow row, string filedName)
       {
           if (row == null || string.IsNullOrEmpty(filedName) || row[filedName] == null) return string.Empty;
           return row[filedName].ToString();
       }

        public static List<string> DistinctToList(this DataTable source, string columnName)
        {
            List<String> columns = new List<string>();
            if(source != null && source.Rows.Count > 0)
            {
                foreach (DataRow dr in source.Rows)
                {
                    string columnValue = dr.GetValue(columnName);
                    if (!string.IsNullOrEmpty(columnValue) && columns.FirstOrDefault(c => c.EqualsCurrentCultureIgnoreCase(columnValue)) == null)
                     columns.Add(columnValue);
                }
            }
            return columns;
        }

        public static int RowCount(this DataTable source, string columnName, string columnValue)
        {
            if (source == null) return 0;
            return source.Select(string.Format("{0}='{1}'", columnName, columnValue)).Length;
        }

        public static int RowCount(this DataTable source, string strWhere)
        {
            if (source == null) return 0;
            return source.Select(strWhere).Length;
        }

        public static string GetCSVFormatData(this DataTable source)
        {
            StringBuilder StringBuilder = new StringBuilder(1000);
            // 写出表头
            foreach (DataColumn DataColumn in source.Columns)
            {
                StringBuilder.Append(DataColumn.ColumnName.ToString() + ",");
            }
            StringBuilder.Append("\n");
            // 写出数据
            foreach (DataRowView dataRowView in source.DefaultView)
            {
                foreach (DataColumn DataColumn in source.Columns)
                {
                    StringBuilder.Append(dataRowView[DataColumn.ColumnName].ToString() + ",");
                }
                StringBuilder.Append("\n");
            }
            return StringBuilder.ToString();
        }

        public static void ExportCSV(this DataTable dataTable, string fileName)
        {
            StreamWriter streamWriter = null;

            streamWriter = new StreamWriter(fileName, false, System.Text.Encoding.GetEncoding("utf-8"));
            
            streamWriter.WriteLine(GetCSVFormatData(dataTable).ToString());
            streamWriter.Flush();
            streamWriter.Close();
        }

        /// 在浏览器中获得CSV格式文件
        public static void GetResponseCSV(this DataTable dataTable, string fileName)
        {
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(GetCSVFormatData(dataTable).ToString());
            HttpContext.Current.Response.End();
        }
    }
}
