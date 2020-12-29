using BODYSHPBLL.ImplBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BODYSHPDAL.DbContext;
using System.Net.Mail;

namespace BODYSHPDAL.ImplDAL
{
    public class EmailMasterDAL
    {
        public static List<EmailListModel> GetData(SessionDataBLL Obj)
        {
            using (var DbContext = new BSSDBEntities())
            {
                var ReqData = DbContext.SP_GetEmailList(Obj.DealerID,Obj.AccountId).ToList();

                List<EmailListModel> EmailList = ReqData.Select(x => new EmailListModel {

                    Id=x.Id,
                    PersonName =x.PersonName,
                    Email_ID=x.Email_ID,
                    IsDeleted=x.IsDeleted,
                    AccountId=x.AccountId,
                    DealerId=x.DealerId,
                    
                }).ToList();
                return EmailList;
            }

        }

        public static string UpDate(EmailListModel Obj)
        {
            using (var DbContext = new BSSDBEntities())
            {
                var ReqData = DbContext.TblEmailMasters.Where(x=>x.Id==Obj.Id).FirstOrDefault();
                ReqData.IsDeleted = Obj.IsDeleted;
                ReqData.PersonName = Obj.PersonName;
                ReqData.Email_ID = Obj.Email_ID;
                ReqData.ModifiedDate = DateTime.Now;
                ReqData.ModifiedBy = Obj.ModifiedBy;
                DbContext.Entry(ReqData).State = System.Data.Entity.EntityState.Modified;
                DbContext.SaveChanges();
               
                return "Updated Successfully";

            }
        }

        public static string AddEmail(EmailListModel Obj)
        {
            using (var DbContext = new BSSDBEntities())
            {
                var ReqData = DbContext.TblEmailMasters.Where(x => x.PersonName == Obj.PersonName && x.AccountId==Obj.AccountId && x.DealerId==Obj.DealerId).FirstOrDefault();
                if (ReqData==null)
                {
                    TblEmailMaster TM=new TblEmailMaster
                    { 
                        IsDeleted = false,
                        PersonName = Obj.PersonName,
                        Email_ID = Obj.Email_ID,
                        AccountId=Obj.AccountId,
                        DealerId=Obj.DealerId,
                        CreationDate = DateTime.Now,
                        CreatedBy = Obj.CreatedBy
                    };
                    DbContext.Entry(TM).State = System.Data.Entity.EntityState.Added;
                    DbContext.SaveChanges();
                
                    return "Updated Successfully";
                }
            else{ 
                return "Already Exixts!";
            }

        }
        }
    }
}
