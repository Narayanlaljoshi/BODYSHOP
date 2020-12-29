using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BODYSHPBLL.ImplBLL;
using BODYSHPDAL.DbContext;
using System.Data.Entity;

namespace BODYSHPDAL.ImplDAL
{
    public class UpdateStatusDAL
    {
        public static string UpdateStatus(UpdateStatusBLL obj,string PhotoUrl)
        {
            long JobCardId = Convert.ToInt64(obj.JobCardId);
            using (var dbContext=new BSSDBEntities())
            {
                var UpdateData = dbContext.TblJobCardDtls.Where(x => x.JobCardId == JobCardId).FirstOrDefault();
                var UpdateHdrData = dbContext.TblJobCardHdrs.Where(x => x.JobCardId == JobCardId).FirstOrDefault();
                if (UpdateData != null)
                {
                    TblJobCardDtl bp = new TblJobCardDtl
                    {
                        JobCardId=obj.JobCardId,
                        No_Of_Panel=obj.NumberOfPanel,
                        PanelReplaced=obj.PanelReplaced,
                        StatusID=obj.StatusID,
                        ContractorId=obj.Contractor_Id,
                        NoOfGlass=obj.Glass,
                        CreatedBy= UpdateData.CreatedBy,
                        CreationDate=UpdateData.CreationDate,
                        ModifiedBy =obj.ModifiedBy ,
                        ModifiedDate=DateTime.Now
                    };
                    dbContext.Entry(bp).State = EntityState.Added;
                    dbContext.SaveChanges();

                    UpdateHdrData.InsuranceCompanyId = obj.InsCompId;
                    UpdateHdrData.PaymentMode = obj.PaymentMode;

                    dbContext.Entry(UpdateHdrData).State = EntityState.Modified;
                    dbContext.SaveChanges();
                    //Updating photo details
                    UpdateStatusDAL.UpdatePhotoDtl(obj.JobCardId, PhotoUrl);

                    return "success";
                }
                else
                    return "Error";
            }
        }

        public static string UpdatePhotoDtl(long JobCardId,string Path)
        {
            using (var DbContext= new BSSDBEntities())
            {
                var Check = DbContext.TblPhotoDtls.Where(x => x.JobCardId == JobCardId && x.PhotoUrl==Path && x.IsDeleted == false).FirstOrDefault();
                if (Check == null)
                {
                    TblPhotoDtl TPD = new TblPhotoDtl
                    {
                        JobCardId = JobCardId,
                        PhotoUrl = Path,
                        IsDeleted = false,
                    };
                    DbContext.Entry(TPD).State = EntityState.Added;
                    DbContext.SaveChanges();

                    return "Updated";
                }
                else
                {
                    return "Already Exists";
                }
            }
        }
    }
}
