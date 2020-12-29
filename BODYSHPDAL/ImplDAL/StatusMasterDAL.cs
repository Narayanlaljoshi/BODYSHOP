using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BODYSHPDAL.DbContext;
using BODYSHPBLL.ImplBLL;

namespace BODYSHPDAL.ImplDAL
{
    public class StatusMasterDAL
    {
        public static List<SpGetStatus_Result> GetData(SessionDataBLL Obj)
        {
            List<SpGetStatus_Result> StatusResult = null;
            using (var dbContext = new BSSDBEntities())
            {
                var ReqData = dbContext.SpGetStatus(Obj.DealerID,Obj.AccountId);
                StatusResult = ReqData.ToList();
            }
            return StatusResult;
        }
        public static void Post(StatusWiseBLL Obj)
        {

            using (var dbContext = new BSSDBEntities())
            {
                var Check = dbContext.TblStatus.Where(x => x.StatusName == Obj.StatusName && x.DealerId==Obj.DealerID && x.AccountId==Obj.AccountId).FirstOrDefault();

                if (Check == null)
                {
                    TblStatu tb = new TblStatu
                    {
                        StatusName= Obj.StatusName,
                        ShortName=Obj.ShortName,
                        StatusCode= Obj.StatusCode,
                        DealerId=Obj.DealerID,
                        AccountId=Obj.AccountId,
                        IsDeleted=false,
                        CreationDate=DateTime.Now
                       
                    };
                    dbContext.Entry(tb).State = System.Data.Entity.EntityState.Added;
                    dbContext.SaveChanges();
                }



            }

        }

        public static void Update(StatusWiseBLL obj )
        {
            using (var dbContext = new BSSDBEntities())
            {
                var Check = dbContext.TblStatus.Where(x => x.StatusID == obj.StatusID && x.AccountId==obj.AccountId && x.DealerId==obj.DealerID).FirstOrDefault();

                if (Check != null)
                {
                    Check.StatusName = obj.StatusName;
                    Check.ShortName = obj.ShortName;
                    Check.StatusCode = obj.StatusCode;
                    Check.ModifiedDate = DateTime.Now;
                    dbContext.Entry(Check).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                }
                
            }
        }

        public static void Delete(long StatusID)
        {
            using (var dbContext = new BSSDBEntities())
            {
                var Check = dbContext.TblStatus.Where(x => x.StatusID == StatusID).FirstOrDefault();

                if (Check != null)
                {
                    Check.IsDeleted = true;
                    dbContext.Entry(Check).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();

                }

            }

        }

        public static List<spGetStatusPopUpDetails_Result> StatusPopup(long Sno)
        {
            List<spGetStatusPopUpDetails_Result> StatusPopupData = null;
            using (var dbContext = new BSSDBEntities())
            {
                var ReqData = dbContext.spGetStatusPopUpDetails(Sno);

                StatusPopupData = ReqData.ToList();

            }
            return StatusPopupData;
        }

        public static List<spGetStatusDetails_Result> Detail(long Sno)
        {

            List<spGetStatusDetails_Result> JobCardDetail = null;

            using (var dbContext = new BSSDBEntities())
            {
                var ReqData = dbContext.spGetStatusDetails(Sno);

                JobCardDetail = ReqData.ToList();

            }
            return JobCardDetail;
        }


    }
}
