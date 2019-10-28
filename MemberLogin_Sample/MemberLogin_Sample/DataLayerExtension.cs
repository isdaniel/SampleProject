using System;
using System.Collections.Generic;
using System.Data;

namespace MemberLogin_Sample
{
    public static class DataLayerExtension
    {
        public static IEnumerable<T> ExecuteReader<T>(this IDbCommand cmd,Func<IDataReader,T> func)
        {
            List<T> result = new List<T>();

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    result.Add(func(dr));
                }
            }

            return result;
        }
    }
}