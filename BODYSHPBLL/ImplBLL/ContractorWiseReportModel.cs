using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BODYSHPBLL.ImplBLL
{
    public class ContractorWiseReportModel
    {
        public long Contractor_Id { get; set; }
        public string ContractorName { get; set; }
        public int? Total { get; set; }
        public List<StatusList> StatusList { get;set; }
    }
    public class StatusList
    {
        public long? StatusID { get; set; }
        public string StatusName { get; set; }
        public string StatusCode { get; set; }
        public Nullable<int> JobCards { get; set; }
        
    }

    public class ReportInput
    {
        public List<ContractorWiseReportModel> ContractorWiseReport { get; set; }
        public SessionDataBLL SessionData { get; set; }
    }
}
