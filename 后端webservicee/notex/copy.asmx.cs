using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Web.Security;
using System.Data;
using System.Text;
using System.IO;

namespace notex
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
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
                string sqll = "UPDATE `" + table_name + "` SET upable = '1' WHERE NOW() > next_time";
                DbHelperSQL.ExecuteNonQuery(sqll);
                string sql = "SELECT c,rest,upable FROM " + table_name + " WHERE rest <> '0' ORDER BY rest DESC";
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
        public String repeat(string c, string table_name, string rest, string upable)
        {
            string restt = rest;
            string reslut = "";
            string next_time;
            try
            {
                if ((upable == "1") && (rest == "11"))
                {
                    upable = "0";
                    next_time = DateTime.Now.AddHours(0.75).ToString("yyyy/MM/dd HH:mm");
                    string sql = "UPDATE `" + table_name + "` SET next_time='" + next_time + "',upable='" + upable + "' WHERE c='" + c + "'";
                    DbHelperSQL.ExecuteNonQuery(sql);
                }
                if ((upable == "1") && (rest == "10"))
                {
                    rest = "11";
                    upable = "0";
                    next_time = DateTime.Now.AddHours(0.75).ToString("yyyy/MM/dd HH:mm");
                    string sql = "UPDATE `" + table_name + "` SET rest='" + rest + "',next_time='" + next_time + "',upable='" + upable + "' WHERE c='" + c + "'";
                    DbHelperSQL.ExecuteNonQuery(sql);
                }
                if ((upable == "1") && (rest == "9"))
                {
                    rest = "10";
                    upable = "0";
                    next_time = DateTime.Now.AddHours(0.75).ToString("yyyy/MM/dd HH:mm");
                    string sql = "UPDATE `" + table_name + "` SET rest='" + rest + "',next_time='" + next_time + "',upable='" + upable + "' WHERE c='" + c + "'";
                    DbHelperSQL.ExecuteNonQuery(sql);
                }
                if ((upable == "1") && (rest == "8"))
                {
                    rest = "9";
                    upable = "0";
                    next_time = DateTime.Now.AddHours(1.5).ToString("yyyy/MM/dd HH:mm");
                    string sql = "UPDATE `" + table_name + "` SET rest='" + rest + "',next_time='" + next_time + "',upable='" + upable + "' WHERE c='" + c + "'";
                    DbHelperSQL.ExecuteNonQuery(sql);
                }
                if ((upable == "1") && (rest == "7"))
                {
                    rest = "8";
                    upable = "0";
                    next_time = DateTime.Now.AddHours(3).ToString("yyyy/MM/dd HH:mm");
                    string sql = "UPDATE `" + table_name + "` SET rest='" + rest + "',next_time='" + next_time + "',upable='" + upable + "' WHERE c='" + c + "'";
                    DbHelperSQL.ExecuteNonQuery(sql);
                }
                if ((upable == "1") && (rest == "6"))
                {
                    rest = "7";
                    upable = "0";
                    next_time = DateTime.Now.AddHours(6).ToString("yyyy/MM/dd HH:mm");
                    string sql = "UPDATE `" + table_name + "` SET rest='" + rest + "',next_time='" + next_time + "',upable='" + upable + "' WHERE c='" + c + "'";
                    DbHelperSQL.ExecuteNonQuery(sql);
                }
                if ((upable == "1") && (rest == "5"))
                {
                    rest = "6";
                    upable = "0";
                    next_time = DateTime.Now.AddHours(12).ToString("yyyy/MM/dd HH:mm");
                    string sql = "UPDATE `" + table_name + "` SET rest='" + rest + "',next_time='" + next_time + "',upable='" + upable + "' WHERE c='" + c + "'";
                    DbHelperSQL.ExecuteNonQuery(sql);
                }
                if ((upable == "1") && (rest == "4"))
                {
                    rest = "5";
                    upable = "0";
                    next_time = DateTime.Now.AddHours(24).ToString("yyyy/MM/dd HH:mm");
                    string sql = "UPDATE `" + table_name + "` SET rest='" + rest + "',next_time='" + next_time + "',upable='" + upable + "' WHERE c='" + c + "'";
                    DbHelperSQL.ExecuteNonQuery(sql);
                }
                if ((upable == "1") && (rest == "3"))
                {
                    rest = "4";
                    upable = "0";
                    next_time = DateTime.Now.AddHours(48).ToString("yyyy/MM/dd HH:mm");
                    string sql = "UPDATE `" + table_name + "` SET rest='" + rest + "',next_time='" + next_time + "',upable='" + upable + "' WHERE c='" + c + "'";
                    DbHelperSQL.ExecuteNonQuery(sql);
                }
                if ((upable == "1") && (rest == "2"))
                {
                    rest = "3";
                    upable = "0";
                    next_time = DateTime.Now.AddHours(96).ToString("yyyy/MM/dd HH:mm");
                    string sql = "UPDATE `" + table_name + "` SET rest='" + rest + "',next_time='" + next_time + "',upable='" + upable + "' WHERE c='" + c + "'";
                    DbHelperSQL.ExecuteNonQuery(sql);
                }
                if ((upable == "1") && (rest == "1"))
                {
                    rest = "6";
                    upable = "0";
                    next_time = DateTime.Now.AddHours(6).ToString("yyyy/MM/dd HH:mm");
                    string sql = "UPDATE `" + table_name + "` SET rest='" + rest + "',next_time='" + next_time + "',upable='" + upable + "' WHERE c='" + c + "'";
                    DbHelperSQL.ExecuteNonQuery(sql);
                }
                if (rest != restt)
                {
                    reslut = "repeat success";
                }
                else
                {
                    reslut = "repeat failed";
                }
            }
            catch
            {
                reslut = "program exception ";
            }
            return reslut;
        }
        [WebMethod]
        /// <summary>
        /// 获取故障说明
        public String pass(string c, string table_name, string rest, string upable)
        {
            string restt = rest;
            string reslut = "";
            string next_time;
            try
            {
                if ((upable == "1") && (rest == "11"))
                {
                    rest = "10";
                    upable = "0";
                    next_time = DateTime.Now.AddHours(0.75).ToString("yyyy/MM/dd HH:mm");
                    string sql = "UPDATE `" + table_name + "` SET rest='" + rest + "',next_time='" + next_time + "',upable='" + upable + "' WHERE c='" + c + "'";
                    DbHelperSQL.ExecuteNonQuery(sql);
                }
                if ((upable == "1") && (rest == "10"))
                {
                    rest = "9";
                    upable = "0";
                    next_time = DateTime.Now.AddHours(1.5).ToString("yyyy/MM/dd HH:mm");
                    string sql = "UPDATE `" + table_name + "` SET rest='" + rest + "',next_time='" + next_time + "',upable='" + upable + "' WHERE c='" + c + "'";
                    DbHelperSQL.ExecuteNonQuery(sql);
                }
                if ((upable == "1") && (rest == "9"))
                {
                    rest = "8";
                    upable = "0";
                    next_time = DateTime.Now.AddHours(3).ToString("yyyy/MM/dd HH:mm");
                    string sql = "UPDATE `" + table_name + "` SET rest='" + rest + "',next_time='" + next_time + "',upable='" + upable + "' WHERE c='" + c + "'";
                    DbHelperSQL.ExecuteNonQuery(sql);
                }
                if ((upable == "1") && (rest == "8"))
                {
                    rest = "7";
                    upable = "0";
                    next_time = DateTime.Now.AddHours(6).ToString("yyyy/MM/dd HH:mm");
                    string sql = "UPDATE `" + table_name + "` SET rest='" + rest + "',next_time='" + next_time + "',upable='" + upable + "' WHERE c='" + c + "'";
                    DbHelperSQL.ExecuteNonQuery(sql);
                }
                if ((upable == "1") && (rest == "7"))
                {
                    rest = "6";
                    upable = "0";
                    next_time = DateTime.Now.AddHours(12).ToString("yyyy/MM/dd HH:mm");
                    string sql = "UPDATE `" + table_name + "` SET rest='" + rest + "',next_time='" + next_time + "',upable='" + upable + "' WHERE c='" + c + "'";
                    DbHelperSQL.ExecuteNonQuery(sql);
                }
                if ((upable == "1") && (rest == "6"))
                {
                    rest = "5";
                    upable = "0";
                    next_time = DateTime.Now.AddHours(24).ToString("yyyy/MM/dd HH:mm");
                    string sql = "UPDATE `" + table_name + "` SET rest='" + rest + "',next_time='" + next_time + "',upable='" + upable + "' WHERE c='" + c + "'";
                    DbHelperSQL.ExecuteNonQuery(sql);
                }
                if ((upable == "1") && (rest == "5"))
                {
                    rest = "4";
                    upable = "0";
                    next_time = DateTime.Now.AddHours(48).ToString("yyyy/MM/dd HH:mm");
                    string sql = "UPDATE `" + table_name + "` SET rest='" + rest + "',next_time='" + next_time + "',upable='" + upable + "' WHERE c='" + c + "'";
                    DbHelperSQL.ExecuteNonQuery(sql);
                }
                if ((upable == "1") && (rest == "4"))
                {
                    rest = "3";
                    upable = "0";
                    next_time = DateTime.Now.AddHours(96).ToString("yyyy/MM/dd HH:mm");
                    string sql = "UPDATE `" + table_name + "` SET rest='" + rest + "',next_time='" + next_time + "',upable='" + upable + "' WHERE c='" + c + "'";
                    DbHelperSQL.ExecuteNonQuery(sql);
                }
                if ((upable == "1") && (rest == "3"))
                {
                    rest = "2";
                    upable = "0";
                    next_time = DateTime.Now.AddHours(192).ToString("yyyy/MM/dd HH:mm");
                    string sql = "UPDATE `" + table_name + "` SET rest='" + rest + "',next_time='" + next_time + "',upable='" + upable + "' WHERE c='" + c + "'";
                    DbHelperSQL.ExecuteNonQuery(sql);
                }
                if ((upable == "1") && (rest == "2"))
                {
                    rest = "1";
                    upable = "0";
                    next_time = DateTime.Now.AddHours(384).ToString("yyyy/MM/dd HH:mm");
                    string sql = "UPDATE `" + table_name + "` SET rest='" + rest + "',next_time='" + next_time + "',upable='" + upable + "' WHERE c='" + c + "'";
                    DbHelperSQL.ExecuteNonQuery(sql);
                }
                if ((upable == "1") && (rest == "1"))
                {
                    rest = "0";
                    upable = "0";
                    string sql = "UPDATE `" + table_name + "` SET rest='" + rest + "',upable='" + upable + "' WHERE c='" + c + "'";
                    DbHelperSQL.ExecuteNonQuery(sql);
                }     
                if (rest != restt)
                {
                    reslut = "pass success";
                }
                else
                {
                    reslut = "pass failed";
                }
                if (rest == "0")
                {
                    reslut = "you win";
                }
            }
            catch
            {
                reslut = "program exception ";
            }
            return reslut;

        }
        [WebMethod]
        /// <summary>
        /// 获取故障说明
        public String add(int PageIndex, int PageSize, string c, string table_name)
        {
            string rest = "11";
            string upable = "0";
            string reslut1 = "";
            string reslut = "";
            DataTable dt = new DataTable();
            try
            {
                string sql = "INSERT INTO `" + table_name + "` (rest, c, next_time, upable) VALUES ('" + rest + "', '" + c + "', '" + DateTime.Now.AddHours(0.75).ToString("yyyy/MM/dd HH:mm") + "', '" + upable + "')";
                int a = DbHelperSQL.ExecuteNonQuery(sql);
                if (a > 0)
                {
                    reslut1 = "add success";
                }
                else
                {
                    reslut1 = "add failed";
                }
                string sqll = "UPDATE `" + table_name + "` SET upable = '1' WHERE NOW() > next_time";
                DbHelperSQL.ExecuteNonQuery(sqll);
                sql = "SELECT c,rest,upable FROM " + table_name + " WHERE rest <> '0' ORDER BY rest DESC";
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
                reslut = "exception "+reslut1;
            }
            return reslut;
        }
        [WebMethod]
        /// <summary>
        /// 获取故障说明
        public String change_answer(string c, string table_name, string answer)
        {
            string reslut = "";
            try
            {
                string sql = "UPDATE `" + table_name + "` SET answer='" + answer + "' WHERE c = '" + c +"'";
                int a = DbHelperSQL.ExecuteNonQuery(sql);
                if (a > 0)
                {
                    reslut = "change success";
                }
                else
                {
                    reslut = "change failed";
                }
            }
            catch
            {
                reslut = "exception";
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
