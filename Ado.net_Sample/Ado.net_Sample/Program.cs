using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado.net_Sample
{
    public class DataModel { 
    }

    class Program
    {
        private static string _conn = ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;

        static void Main(string[] args)
        {
            string password = "test123";
            string sql = @"SELECT UserId,
	  UserName,
	  [PassWord]
FROM dbo.UserAccount
WHERE password = @password;";

            #region Use SqlCommand Version.
            //建立SqlConnection物件
            SqlExecuteReader(password, sql);
            #endregion

            #region Use SqlDataAdapter
            SqlDataAdapter(password, sql);
            #endregion 

            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                var result = conn.Query<DataModel>(sql);
            }

            Console.ReadKey();
        }

        private static void SqlExecuteReader(string parameter, string sql)
        {
            using (var conn = new SqlConnection(_conn))
            {
                //打開與資料庫的連接
                conn.Open();

                //建立Sql的命令類別
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    //預設是 CommandType.Text
                    cmd.CommandType = CommandType.Text;

                    //如果有使用到參數 添加參數
                    var para = new SqlParameter(
                        "@password", SqlDbType.VarChar, 100)
                    {
                        Value = parameter
                    };
                    cmd.Parameters.Add(para);

                    //從料庫中讀取資料
                    using (var dr = cmd.ExecuteReader())
                    {

                        //判斷是否還有下一筆資料
                        while (dr.Read())
                        {
                            Console.WriteLine($"{dr["UserId"]} {dr["UserName"]} {dr["PassWord"]}");
                        }
                    }
                }
            }
        }

        private static void SqlDataAdapter(string parameter, string sql)
        {
            using (var conn = new SqlConnection(_conn))
            {
                //打開與資料庫的連接
                conn.Open();

                //建立Sql的命令類別
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    //預設是 CommandType.Text
                    cmd.CommandType = CommandType.Text;

                    //如果有使用到參數 添加參數
                    var para = new SqlParameter(
                        "@password", SqlDbType.VarChar, 100)
                    {
                        Value = parameter
                    };
                    cmd.Parameters.Add(para);

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
        }
    }
}
