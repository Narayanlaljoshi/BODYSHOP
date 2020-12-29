using BODYSHPDAL.DbContext;
using BODYSHPDAL.ImplDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BODYSHPBLL.ImplBLL;

namespace BODYSHP.Controllers
{
    public class ReadyJobCardsController : ApiController
    {

        [HttpPost]
        public List<SpReadyForDeliver_Result> GetData(SessionDataBLL Obj)
        {
            return ReadyJobCardsDAL.GetData(Obj);
        }

        [HttpPost]
        public List<SpGetStatusReadyDeliver_Result> GetStatusName(SessionDataBLL Obj)
        {
            return ReadyJobCardsDAL.GetStatusName(Obj);
        }


        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}