using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Text;
using System.IO;

namespace notex
{
    /// <summary>
    /// menu 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class menu : System.Web.Services.WebService
    {
        [WebMethod]
        /// <summary>
        /// 获取故障说明
        public String get(int PageIndex, int PageSize)
        {
            DataTable dt = new DataTable();
            string reslut = "";
            try
            {
                string sql = "SELECT table_name FROM information_schema.tables WHERE table_schema='mvc_crud'";
                dt = DbHelperSQL.ExecuteDataTable(sql);
                DataTable newdt = dt.Copy();
                newdt.Clear();
                int rowbegin = (PageIndex - 1) * PageSize;
                int rowend = PageIndex * PageSize;
                if (rowbegin >= dt.Rows.Count)
                {
                    reslut = DataTableToJson(newdt);//源数据记录数小于等于要显示的记录，直接返回dt
                }
                if (rowend > dt.Rows.Count)
                {
                    rowend = dt.Rows.Count;
                }
                for (int i = rowbegin; i <= rowend - 1; i++)
                {
                    DataRow newdr = newdt.NewRow();
                    DataRow dr = dt.Rows[i];
                    foreach (DataColumn column in dt.Columns)
                    {
                        newdr[column.ColumnName] = dr[column.ColumnName];
                    }
                    newdt.Rows.Add(newdr);
                }
                reslut = DataTableToJson(newdt);
            }
            catch
            {
                reslut = "exception";
            }
            return reslut;
        }
        [WebMethod]
        /// <summary>
        /// 获取故障说明
        public String add(string table_name, int PageIndex, int PageSize)
        {
            string reslut1 = "";
            string reslut = "";
            string sql = "";
            DataTable dt = new DataTable();
            try
            {
                if (table_name.StartsWith("note"))
                {
                    sql = "CREATE TABLE `" + table_name + "` (`o` VARCHAR(10) DEFAULT NULL,`c` VARCHAR(130) DEFAULT NULL,`l` VARCHAR(130) DEFAULT NULL) ENGINE=InnoDB DEFAULT CHARSET=utf8";
                }
                if (table_name.StartsWith("copy"))
                {
                    sql = "CREATE TABLE `" + table_name + "` (`rest` varchar(2) DEFAULT NULL,`c` varchar(100) DEFAULT NULL,`create_time` varchar(16) DEFAULT NULL,`last_time` varchar(16) DEFAULT NULL,`rest_time` varchar(10) DEFAULT NULL,`label` varchar(10) DEFAULT NULL,`upable` varchar(2) DEFAULT NULL,`answer` varchar(80) DEFAULT NULL) ENGINE = InnoDB DEFAULT CHARSET = utf8";
                }
                DbHelperSQL.ExecuteNonQuery(sql);              
            }
            catch
            {
                reslut1 = "add_exception";
            }
            try
            {
                sql = "SELECT table_name FROM information_schema.tables WHERE table_schema='mvc_crud'";
                dt = DbHelperSQL.ExecuteDataTable(sql);
                DataTable newdt = dt.Copy();
                newdt.Clear();
                int rowbegin = (PageIndex - 1) * PageSize;
                int rowend = PageIndex * PageSize;
                if (rowbegin >= dt.Rows.Count)
                {
                    reslut = DataTableToJson(newdt);//源数据记录数小于等于要显示的记录，直接返回dt
                }
                if (rowend > dt.Rows.Count)
                {
                    rowend = dt.Rows.Count;
                }
                for (int i = rowbegin; i <= rowend - 1; i++)
                {
                    DataRow newdr = newdt.NewRow();
                    DataRow dr = dt.Rows[i];
                    foreach (DataColumn column in dt.Columns)
                    {
                        newdr[column.ColumnName] = dr[column.ColumnName];
                    }
                    newdt.Rows.Add(newdr);
                }
                reslut = DataTableToJson(newdt);
            }
            catch
            {
                reslut = "refresh_exception" + reslut1;
            }
            return reslut;
        }
        public static string DataTableToJson(DataTable table)
        {
            var JsonString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JsonString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JsonString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JsonString.Append("}");
                    }
                    else
                    {
                        JsonString.Append("},");
                    }
                }
                JsonString.Append("]");
            }
            return JsonString.ToString();
        }
    }
}
