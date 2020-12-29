using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BODYSHPDAL.DbContext;

namespace BODYSHPDAL.ImplDAL
{
    public class GetStatusDetailsDAL
    {
        public static List<spGetStatusDetails_Result> Get(long Sno)
        {
            List<spGetStatusDetails_Result> Obj = null; ;
            using (var dbContext = new BSSDBEntities())
            {
                var ReqData = dbContext.spGetStatusDetails(Sno);
                Obj = ReqData.ToList();
            }
            return Obj;
        }


    }
}
