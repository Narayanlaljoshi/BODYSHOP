using BODYSHPBLL;
using BODYSHPBLL.ImplBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BODYSHP.Controllers
{
    public class ContractorMasterController : ApiController
    {
        [HttpPost]
        public List<ContractorModel> GetContractor(SessionDataBLL Obj)
        {

            return ContractorDAL.GetContractor(Obj);
        }
        [HttpPost]
        public bool AddContractor(ContractorModel Obj)
        {
            return  ContractorDAL.AddContractor(Obj);

        }
        [HttpPost]
        public bool UpdateContractor(ContractorModel Obj)
        {
            return ContractorDAL.UpdateContractor(Obj);

        }
        [HttpPost]
        public bool Delete(ContractorModel Obj)
        {
            return ContractorDAL.Delete(Obj);

        }
    }
}