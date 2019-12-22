using System.Collections.Generic;

namespace WebApiService.Controllers
{
    public class PatientViewModel
    {
        public IEnumerable<string> RID { get; set; }
    }

    public class ChartNoViewModel
    {
        public IEnumerable<string> ChartNo { get; set; }
    }
}