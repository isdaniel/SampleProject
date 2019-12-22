using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.SqlServer.Server;

namespace HttpSample
{
    public class ApiService
    {
        static string connStr = ConfigurationManager.ConnectionStrings["DbConn"].ToString();
        public IEnumerable<string> GetRID()
        {
            using (var conn = new SqlConnection(connStr))
            {
                //打開連結
                conn.Open();
                return conn.Query<EBMModel>("[dbo].[GetALL_EBM]",commandType:CommandType.StoredProcedure).Select(x=>x.RID);
            }
        }

        public void InsertEBM(IEnumerable<EBMModel> model)
        {
            using (var conn = new SqlConnection(connStr))
            {
                //打開連結
                conn.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@EMB",ConvertToEMBDataRecord(model).AsTableValuedParameter("dbo.utff_EMB"));
                conn.Execute("[dbo].[InsertEBMData]",parameters,commandType:CommandType.StoredProcedure);
            }
        }

        public IEnumerable<SqlDataRecord> ConvertToEMBDataRecord(IEnumerable<EBMModel> parameters){
            var sqlMetaData = new SqlMetaData[]{
                new SqlMetaData("RID", SqlDbType.VarChar,50),
                new SqlMetaData("ChartNo", SqlDbType.VarChar,50),
                new SqlMetaData("STATUS", SqlDbType.VarChar,5),
            };
            var mappingDictionarys = sqlMetaData.ToMappingDictionary();
            var records = new List<SqlDataRecord>();
            if(parameters!=null)
            {
                foreach (var para in parameters){
                    var record = new SqlDataRecord(sqlMetaData);
                    record.SetStringOrNull(mappingDictionarys["RID"], para.RID);
                    record.SetStringOrNull(mappingDictionarys["ChartNo"], para.ChartNo);
                    record.SetStringOrNull(mappingDictionarys["STATUS"], para.Status);
                    records.Add(record);
                }
            }
            return records;
        }

    }

    public static class Ext
    {
        public static void SetStringOrNull(this SqlDataRecord record, int index, string val)
        {
            if (!string.IsNullOrEmpty(val))
                record.SetString(index, val);
            else
                record.SetDBNull(index);
        }
        public static Dictionary<string, int> ToMappingDictionary(this SqlMetaData[] metaData)
        {
            int num = 0;
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            foreach (SqlMetaData sqlMetaData in metaData)
            {
                dictionary.Add(sqlMetaData.Name, num);
                ++num;
            }
            return dictionary;
        }
    }

}