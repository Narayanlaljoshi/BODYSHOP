using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BODYSHPDAL.DbContext;
using BODYSHPDAL.ImplDAL;

namespace BODYSHP.Controllers
{
    public class BodyShopPreviewController : ApiController
    {
        [HttpPost]
        public List<spGetStatusDetails_Result> GetData(long Sno)
        {
            return GetStatusDetailsDAL.Get(Sno);
        }

    }
}