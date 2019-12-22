using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dapper;

namespace WebApiService.Controllers
{
    [RoutePrefix("api/Patient")]
    public class PatientController : ApiController
    {
        private PatientService _patientService = new PatientService();

        [HttpPost]
        [Route("GetPatientRounds")]
        public ApiReturnViewModel<IEnumerable<PatientRoundViewModel>> GetPatientRounds(PatientViewModel viewModel)
        {
            var result  = new ApiReturnViewModel<IEnumerable<PatientRoundViewModel>>()
            {
                Status = "Ok",
                Data = _patientService.GetPatientData(viewModel.RID)
                    .Select(x => new PatientRoundViewModel()
                    {
                        ChartNo = x.ChartNo,
                        Date = DateTime.Now.ToString("yyyyMMdd")
                    }),
                ErrorMessage = ""
            };

            return result;
        }

        [HttpPost]
        [Route("GetPatients")]
        public ApiReturnViewModel<IEnumerable<EBMModel>> GetPatients(ChartNoViewModel viewModel)
        { 
            var result  = new ApiReturnViewModel<IEnumerable<EBMModel>>()
            {
                Status = "Ok",
                Data = _patientService.GetEBMByChartNo(viewModel.ChartNo),
                ErrorMessage = ""
            };

            return result;
        }
    }
}
