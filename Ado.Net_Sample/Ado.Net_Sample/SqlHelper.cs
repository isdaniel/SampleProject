using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Ado.Net_Sample
{
    public class SqlHelper
    {
        private readonly string _conn;

        public SqlHelper(string conn)
        {
            _conn = conn;
        }

        public IEnumerable<T> Query<T>(string sql,
            Func<IDataReader,T> func,
            params SqlParameter[] parameters)
        {

            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteReader(func);
                }
            }
        }
    }
}