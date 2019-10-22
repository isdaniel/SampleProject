using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.AccessControl;

namespace Ado.Net_Sample
{
    public class MemberDao
    {
        private readonly string _conn = ConfigurationManager.ConnectionStrings["DbConn"].ConnectionString;

        public IEnumerable<MemberModel> GetMemberInfo(string memberName)
        {
           string sql = @"SELECT p.id,
       p.[Name],
       p.Age,
       a.AddressName
FROM Person p JOIN [Address] a ON p.Id = a.PId
WHERE p.[Name] = @Name";

            var para = new SqlParameter("@Name", SqlDbType.VarChar, 50)
            {
                Value = memberName
            };

            #region Normal Version
            //            List<MemberModel> result = new List<MemberModel>();

            //            using (var conn = new SqlConnection(_conn))
            //            {
            //                conn.Open();
            //                using (var cmd = conn.CreateCommand())
            //                {
            //                    cmd.CommandText = sql;
            //                    var para = new SqlParameter("@Name", SqlDbType.VarChar, 50)
            //                    {
            //                        Value = memberName
            //                    };
            //                    cmd.Parameters.Add(para);
            //                    using (var dr = cmd.ExecuteReader())
            //                    {
            //                        while (dr.Read())
            //                        {
            //                            result.Add(new MemberModel()
            //                            {
            //                                Id = (int)dr["Id"],
            //                                Name = (string)dr["Name"],
            //                                Age = (int)dr["Age"],
            //                                AddressName = (string)dr["AddressName"]
            //                            });
            //                        }
            //                    }
            //                }
            //            }
            //            return result; 
            #endregion

            #region Clean Version.
            SqlHelper sqlHelper = new SqlHelper(_conn);
            return sqlHelper.Query(sql, (dr) => new MemberModel()
            {
                Id = (int) dr["Id"],
                Name = (string) dr["Name"],
                Age = (int) dr["Age"],
                AddressName = (string) dr["AddressName"]
            },parameters: para);
            #endregion

        }
    }
}