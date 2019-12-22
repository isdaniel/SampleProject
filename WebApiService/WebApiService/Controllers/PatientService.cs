using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.SqlServer.Server;

namespace WebApiService.Controllers
{
    public class PatientService
    {
        private string _conn;

        public PatientService()
        {
            _conn = ConfigurationManager.ConnectionStrings["Conn"].ToString();

        }

        public IEnumerable<EBMModel> GetPatientData(IEnumerable<string> Rid)
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Rid",
                    ConvertToRidDataRecord(Rid).AsTableValuedParameter("dbo.utff_RID"));

                return conn.Query<EBMModel>("dbo.GetEBMByRID",parameters,commandType:CommandType.StoredProcedure);
            }
        }

        private IEnumerable<SqlDataRecord> ConvertToChartNoDataRecord(IEnumerable<string> parameters){
            var sqlMetaData = new SqlMetaData[]{
                new SqlMetaData("chartNo", SqlDbType.VarChar,50),
            };
            var mappingDictionary = sqlMetaData.ToMappingDictionary();
            var records = new List<SqlDataRecord>();
            if(parameters!=null)
            {
                foreach (var para in parameters){
                    var record = new SqlDataRecord(sqlMetaData);
                    record.SetStringOrNull(mappingDictionary["chartNo"], para);
                    records.Add(record);
                }
            }
            return records;
        }

        private IEnumerable<SqlDataRecord> ConvertToRidDataRecord(IEnumerable<string> parameters){
            var sqlMetaData = new SqlMetaData[]{
                new SqlMetaData("RID", SqlDbType.VarChar,50),
            };
            var mappingDictionary = sqlMetaData.ToMappingDictionary();
            var records = new List<SqlDataRecord>();
            if(parameters!=null)
            {
                foreach (var para in parameters){
                    var record = new SqlDataRecord(sqlMetaData);
                    record.SetStringOrNull(mappingDictionary["RID"], para);
                    records.Add(record);
                }
            }
            return records;
        }


        public IEnumerable<EBMModel> GetEBMByChartNo(IEnumerable<string> chartNo)
        {
            using (var conn = new SqlConnection(_conn))
            {
                conn.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@chartNo",
                    ConvertToChartNoDataRecord(chartNo).AsTableValuedParameter("dbo.utff_chartNo"));

                return conn.Query<EBMModel>("dbo.GetEBMByChartNo",parameters,commandType:CommandType.StoredProcedure);
            }
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