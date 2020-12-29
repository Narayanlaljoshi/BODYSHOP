using BODYSHPBLL.ImplBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BODYSHPDAL.DbContext;
namespace BODYSHPDAL.ImplDAL
{
    public class InsuranceCmpDAL
    {
        public static List<InsuranceCmpList> GetInsuranceCmpList(SessionDataBLL Obj)
        {
            using (var DbContext=new BSSDBEntities())
            {
                List<InsuranceCmpList> InsuranceCmpList = null;
                var ReqData = DbContext.SP_GetInsuranceCmpList(Obj.DealerID, Obj.AccountId).ToList();
                InsuranceCmpList = ReqData.Select(x => new InsuranceCmpList
                {
                    InsuranceCompanyId=x.InsuranceCompanyId,
                    CompanyName=x.CompanyName,
                    CompanyCode=x.CompanyCode,
                    IsDeleted=x.IsDeleted,
                    AccountId=x.AccountId,
                    DealerId=x.DealerId
                }).ToList();
                return InsuranceCmpList;
            }
        }
        public static string AddInsuranceCmp(InsuranceCmpList Obj)
        {
            using (var DbContext = new BSSDBEntities())
            {
               
                var ReqData = DbContext.Tbl_InsuranceCmpMaster.Where(x=>x.CompanyName==Obj.CompanyName && x.CompanyCode==Obj.CompanyCode && x.DealerId==Obj.DealerId && x.AccountId==Obj.AccountId && x.IsDeleted==false).FirstOrDefault();
                if (ReqData==null)
                {
                    Tbl_InsuranceCmpMaster TIC = new Tbl_InsuranceCmpMaster {
                        CompanyName=Obj.CompanyName,
                        CompanyCode=Obj.CompanyCode,
                        IsDeleted =false,
                        CreatedBy=Obj.CreatedBy,
                        AccountId=Obj.AccountId,
                        DealerId=Obj.DealerId,
                        CreationDate=DateTime.Now

                    };
                    DbContext.Entry(TIC).State = System.Data.Entity.EntityState.Added;
                    DbContext.SaveChanges();

                    return "Added successfully !";
                }
                else
                {
                    return "Already exist !";
                }
            }
        }
        public static string UpdateInsuranceCmp(InsuranceCmpList Obj)
        {
            using (var DbContext = new BSSDBEntities())
            {

                var ReqData = DbContext.Tbl_InsuranceCmpMaster.Where(x => x.InsuranceCompanyId == Obj.InsuranceCompanyId ).FirstOrDefault();
                if (ReqData != null)
                {
                    ReqData.CompanyName = Obj.CompanyName;
                    ReqData.CompanyCode = Obj.CompanyCode;
                    ReqData.IsDeleted = Obj.IsDeleted;
                    ReqData.ModifiedBy = Obj.CreatedBy;
                    ReqData.AccountId = Obj.AccountId;
                    ReqData.DealerId = Obj.DealerId;
                    ReqData.ModifiedDate = DateTime.Now;
                    
                    DbContext.Entry(ReqData).State = System.Data.Entity.EntityState.Modified;
                    DbContext.SaveChanges();

                    return "Updated successfully !";
                }
                else
                {
                    return "Does not exist !";
                }
            }
        }
    }
}
