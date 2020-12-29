using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BODYSHPBLL.ImplBLL;
using BODYSHPDAL.ImplDAL;

namespace BODYSHP.Controllers
{
    public class AddQueryController : ApiController
    {
        

        // POST api/<controller>
        public void Post([FromBody]AddQueryBLL obj)
        {
            AddQueryDAL.AddQuery(obj);
        }

       
    }
}