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
    public class EmailMasterController : ApiController
    {
        [HttpPost]
        public List<EmailListModel> GetData(SessionDataBLL Obj)
        {
            return EmailMasterDAL.GetData(Obj);
        }

        [HttpPost]
        public string UpDate(EmailListModel Obj)
        {
            return EmailMasterDAL.UpDate(Obj);
        }
        [HttpPost]
        public string AddEmail(EmailListModel Obj)
        {
            return EmailMasterDAL.AddEmail(Obj);
        }

    }
}