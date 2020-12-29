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
    public class BrandMasterController : ApiController
    {
        [HttpPost]
        public List<spGetBrandList_Result> GetData()
        {
            return BrandMasterDAL.BrandList();

        }

        [HttpPost]
        public void Post(string BrandName,long UserId)
        {
            BrandMasterDAL.Post(BrandName, UserId);

        }
        [HttpPost]
        public void Update(string BrandName,long BrandId)
        {
            BrandMasterDAL.Update(BrandName, BrandId);

        }
        [HttpPost]
        public void Delete(long BrandId)
        {
            BrandMasterDAL.Delete(BrandId);

        }
    }
}