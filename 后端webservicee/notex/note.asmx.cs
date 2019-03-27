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
    /// WebService2 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WebService2 : System.Web.Services.WebService
    {
        [WebMethod]
        /// <summary>
        /// 获取故障说明
        public String get(int PageIndex, int PageSize, string table_name)
        {
            DataTable dt = new DataTable();
            string reslut = "";
            try
            {
                string sql = "SELECT o, c FROM " + table_name + " ORDER BY o,create_time DESC" ;
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
        /// 查询车辆信息       
        public String delete(string c, string table_name)
        {
            string reslut = "";
            try
            {
                string sql = "DELETE FROM " + table_name + " WHERE c='" + c + "'";
                int a = DbHelperSQL.ExecuteNonQuery(sql);
                if (a > 0)
                {
                    reslut = "delete success";
                }
                else
                {
                    reslut = "delete failed";
                }
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
        public String update(string c, string new_c, string table_name)
        {
            DataTable dt = new DataTable();
            string o = "1";
            if (new_c.EndsWith("~"))
            {
                o = "0";
            }
            string reslut = "";
            try
            {
                string sql = "UPDATE " + table_name + " SET c='" + new_c + "',o ='" + o + "',create_time ='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + "' WHERE c='" + c + "'";
                int a = DbHelperSQL.ExecuteNonQuery(sql);
                if (a > 0)
                {
                    reslut = "update success";
                }
                else
                {
                    reslut = "update failed";
                }             
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
        public String add(int PageIndex, int PageSize, string c, string table_name)
        {
            string o = "";
            string reslut1 = "";
            string reslut = "";
            DataTable dt = new DataTable();
            if (c.EndsWith("~"))
            {
                o = "0";
            }
            else {
                o = "1";
            }
            try
            {
                
                string sql = "INSERT INTO "+ table_name +" (o, c, create_time) VALUES ('" + o + "', '" + c + "', '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm") + "')";
                int a = DbHelperSQL.ExecuteNonQuery(sql);
                if (a > 0)
                {
                    reslut1 = "add_success";
                }
                else
                {
                    reslut1 = "add_failed";
                }
                sql = "SELECT*FROM`" + table_name + "`ORDER BY o,create_time DESC";
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
                reslut = "refresh_exception " + reslut1;
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
