using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Sql_Injection
{
    class Program
    {
        private static string _conn = ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;

        static void Main(string[] args)
        {

            Console.WriteLine("請輸入使用者帳號");
            string userName = Console.ReadLine();
            Console.WriteLine("請輸入使用者密碼");
            string passWord = Console.ReadLine(); 

            //SqlHelper sqlHelper = new SqlHelper(_conn);

            IEnumerable<MemberAccount> memberAccounts;

            string sql = $@"SELECT *
FROM dbo.UserAccount
WHERE UserName = '{userName}' AND [PassWord] = '{passWord}'";

            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                memberAccounts = conn.Query<MemberAccount>(sql);
            }

            foreach (var item in memberAccounts)
            {
                Console.WriteLine($"UserName:{item.UserName}");
            }
            
            Console.ReadKey();
        }
    }
}
