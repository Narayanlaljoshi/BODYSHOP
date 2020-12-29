using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BODYSHPDAL.DbContext;

using System.Web.Http;
using BODYSHPBLL.ImplBLL;
using BODYSHPDAL.ImplDAL;

namespace BODYSHP.Controllers
{
    public class GridViewController : ApiController
    {
        [System.Web.Http.HttpPost]
        public List<CompleteDetailsModel> GetData(SessionDataBLL Obj)
        {
            return GridViewDAL.GetData(Obj);
        }
        public List<JobcardDetails> GetDetailForJobcard(string JobcardNo)
        {
            return GridViewDAL.GetDetailForJobcard(JobcardNo);
        }
    }
}
