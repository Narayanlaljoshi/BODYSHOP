using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BODYSHPDAL.DbContext;
using BODYSHPDAL.ImplDAL;
using BODYSHPBLL.ImplBLL;

namespace BODYSHP.Controllers
{
    public class StatusWiseController : ApiController
    {
        [HttpPost]
        public List<SpStatusWiseDtl_Result> GetData(StatusWiseBLL obj)
        {
            return ReportsDAL.GetData(obj);

        }
        [HttpPost]
        public List<SpGetDateWiseDetails_Result> GetDateWiseDtl(DateWiseReportBLL obj)
        {

            return ReportsDAL.GetData(obj);
        }
        [HttpPost]
        public List<SpContractorWiseDtl_Result> GetContractorWiseDtl(ContractorModel obj)
        {

            return ReportsDAL.GetContractorWiseDtl(obj);
        }
        [HttpPost]
        public List<ContractorWiseReportModel> GetContractorWiseReport(ContractorModel obj)
        {
            return ReportsDAL.GetContractorWiseReport(obj);   
        }
        [HttpPost]
        public List<MonthWiseReport> GetMonthWiseReport(MonthWiseInput obj)
        {
            return ReportsDAL.GetMonthWiseReport(obj);
        }
        [HttpPost]
        public string SendEmail(ReportInput Obj)
        {
             return ReportsDAL.SendEmail(Obj);
        }
    }
}