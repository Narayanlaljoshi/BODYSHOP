using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BODYSHPDAL.DbContext;
using BODYSHPBLL.ImplBLL;

namespace BODYSHPDAL.ImplDAL
{
    public class ReadyJobCardsDAL
    {
        
        public static List<SpReadyForDeliver_Result> GetData(SessionDataBLL Obj)
        {
            List<SpReadyForDeliver_Result> spData = null;
            using (var dbContext=new BSSDBEntities())
            {
                var ReqData = dbContext.SpReadyForDeliver(Obj.DealerID,Obj.AccountId);
                spData = ReqData.ToList();

            }
            return spData;
        }

        public static List<SpGetStatusReadyDeliver_Result> GetStatusName(SessionDataBLL Obj)
        {
            List<SpGetStatusReadyDeliver_Result> spData = null;
            using (var dbContext = new BSSDBEntities())
            {
                var ReqData = dbContext.SpGetStatusReadyDeliver(Obj.DealerID, Obj.AccountId);
                spData = ReqData.ToList();

            }
            return spData;
        }

    }
}
