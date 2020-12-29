using BODYSHPBLL.ImplBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BODYSHPDAL.DbContext;

namespace BODYSHPDAL.ImplDAL
{
    public class DashBoardDAL
    {
        public static DashBoardBLL GetData(SessionDataBLL Obj)
        {
            using(var dbContext= new BSSDBEntities())
            {
                var Total = dbContext.SpTotalCount(Obj.DealerID, Obj.AccountId).FirstOrDefault();
                var Delivered = dbContext.SpDeliveredCount(Obj.DealerID, Obj.AccountId).FirstOrDefault();
                var Pending = dbContext.SpPendingCount(Obj.DealerID, Obj.AccountId).FirstOrDefault();
                var Ready = dbContext.SpReadyCount(Obj.DealerID, Obj.AccountId).FirstOrDefault();

                
                DashBoardBLL obj = new DashBoardBLL()
                {
                    Total=Convert.ToInt64(Total),
                    Delivered= Convert.ToInt64(Delivered),
                    Pending= Convert.ToInt64(Pending),
                    Ready=Convert.ToInt64(Ready)
                };

                return obj;

            }



        }
    }
}
