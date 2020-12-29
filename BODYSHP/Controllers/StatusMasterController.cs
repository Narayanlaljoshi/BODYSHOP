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
    public class StatusMasterController : ApiController
    {

        [HttpPost]
        public List<SpGetStatus_Result> GetData(SessionDataBLL Obj)
        {
            return  StatusMasterDAL.GetData(Obj);
        }

        [HttpPost]
        public void Post(StatusWiseBLL Obj )
        {
            StatusMasterDAL.Post(Obj);

        }
        [HttpPost]
        public void Update(StatusWiseBLL obj)
        {
            StatusMasterDAL.Update(obj);

        }
        [HttpPost]
        public void Delete(long StatusID)
        {
            StatusMasterDAL.Delete(StatusID);

        }

        [HttpPost]
        public List<spGetStatusPopUpDetails_Result> StatusPopup(long Sno)
        {
            return StatusMasterDAL.StatusPopup(Sno);
        }

        [HttpPost]
        public List<spGetStatusDetails_Result> Detail(long Sno)
        {
            return StatusMasterDAL.Detail(Sno);
        }



        [HttpPost]
        public List<SpGetStatus_Result> GetStatusName(SessionDataBLL Obj)
        {
            return StatusMasterDAL.GetData(Obj);
        }
    }
}