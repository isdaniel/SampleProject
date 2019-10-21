using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DapperConsole
{
    class Program
    {
        //取得建立連接字串
        static string connStr = ConfigurationManager.ConnectionStrings["DbConn"].ToString();

        static void Main(string[] args)
        {
            
            foreach (var item in QueryFromDb<StudentModel>("SELECT Age,Name FROM dbo.Student"))
            {
                Console.WriteLine(item.Age);
            }

            Console.ReadKey();
        }

        private static IEnumerable<T> QueryFromDb<T>(string sql)
        {
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                return conn.Query<T>(sql);
            }
        }
    }

    public class StudentModel
    {
        public string Name { get; set; }
        public int? Age { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
