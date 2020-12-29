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
    public class DashBoardController : ApiController
    {
        [HttpPost]
        public DashBoardBLL GetData(SessionDataBLL Obj)

        {
            return DashBoardDAL.GetData(Obj);
        }


    }
}