using BODYSHPBLL.ImplBLL;
using BODYSHPDAL.ImplDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BODYSHP.Controllers
{
    public class InsuranceCmpController : ApiController
    {
        [HttpPost]
        public List<InsuranceCmpList> GetInsuranceCmpList(SessionDataBLL Obj)
        {
            return InsuranceCmpDAL.GetInsuranceCmpList(Obj);
        }
        [HttpPost]
        public string AddInsuranceCmp(InsuranceCmpList Obj)
        {
            return InsuranceCmpDAL.AddInsuranceCmp(Obj);
        }
        [HttpPost]
        public string UpdateInsuranceCmp(InsuranceCmpList Obj)
        {
            return InsuranceCmpDAL.UpdateInsuranceCmp(Obj);
        }

    }
}