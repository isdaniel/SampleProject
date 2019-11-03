using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql_Injection
{
    class Program
    {
        private static string _conn = ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;

        static void Main(string[] args)
        {
            string userName = Console.ReadLine();
            string passWord = Console.ReadLine(); 

            SqlHelper sqlHelper = new SqlHelper(_conn);

            string sql = $@"SELECT *
FROM dbo.UserAccount
WHERE UserName = '{userName}' AND [PassWord] = '{passWord}'";

            var userAccounts = sqlHelper.Query(sql, (dr) =>
            {
                MemberAccount model = new MemberAccount()
                {
                    UserID = (int)dr["UserID"],
                    PassWord = (string)dr["PassWord"],
                    UserName = (string)dr["UserName"]
                };
                return model;
            });

            Console.ReadKey();
        }
    }
}
