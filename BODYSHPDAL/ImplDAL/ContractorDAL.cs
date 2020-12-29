using BODYSHPBLL.ImplBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BODYSHPDAL.DbContext;


namespace BODYSHPBLL
{
    public class ContractorDAL
    {
        public static List<ContractorModel> GetContractor(SessionDataBLL Obj)
        {
            using (var dbContext=new BSSDBEntities() )
            {
                var ReqData = dbContext.tblContractorMasters.Where(x => x.DealerID == Obj.DealerID && x.AccountID == Obj.AccountId && x.IsDeleted == false).ToList();


                List <ContractorModel> ContractorData = ReqData.Select(x => new ContractorModel()
                {
                    Contractor_Id=x.Contractor_Id,
                    ContractorCode=x.ContractorCode,
                    ContractorName=x.ContractorName,
                    IsDeleted=x.IsDeleted,
                    Address=x.Address,
                    Phone=x.Phone,
                    CreatedBy=x.CreatedBy,
                    CreationDate=x.CreationDate,
                    ModifiedBy=x.ModifiedBy
                    
                }).ToList();

                return ContractorData;

            }
        }

        public static bool AddContractor(ContractorModel Obj)
        {
            using (var dbContext= new BSSDBEntities())
            {
                var Check = dbContext.tblContractorMasters.Where(x => x.ContractorCode == Obj.ContractorCode && x.DealerID == Obj.DealerID && x.AccountID == Obj.AccountID).FirstOrDefault();
                if (Check == null)
                {
                    tblContractorMaster Cm = new tblContractorMaster()
                    {
                        ContractorName = Obj.ContractorName,
                        ContractorCode = Obj.ContractorCode,
                        CreatedBy = Obj.CreatedBy,
                        AccountID = Obj.AccountID,
                        IsDeleted = false,
                        Address = Obj.Address,
                        Phone=Obj.Phone,
                        CreationDate = DateTime.Now,
                        DealerID = Obj.DealerID
                    };
                    dbContext.Entry(Cm).State = System.Data.Entity.EntityState.Added;
                    dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }   

        }


        public static bool UpdateContractor(ContractorModel Obj)
        {
            using (var dbContext = new BSSDBEntities())
            {
                var Check = dbContext.tblContractorMasters.Where(x => x.ContractorCode == Obj.ContractorCode && x.DealerID == Obj.DealerID && x.AccountID == Obj.AccountID).FirstOrDefault();
                if (Check != null)
                {

                    Check.ContractorName = Obj.ContractorName;
                    Check.ContractorCode = Obj.ContractorCode;
                    Check.CreatedBy = Obj.CreatedBy;
                    Check.AccountID = Obj.AccountID;
                    Check.IsDeleted = false;
                    Check.Address = Obj.Address;
                    Check.Phone = Obj.Phone;
                    Check.CreationDate = DateTime.Now;
                    Check.DealerID = Obj.DealerID;
                   
                    dbContext.Entry(Check).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }

        }

        public static bool Delete(ContractorModel Obj)
        {
            using (var dbContext = new BSSDBEntities())
            {
                var Check = dbContext.tblContractorMasters.Where(x=>x.Contractor_Id==Obj.Contractor_Id).FirstOrDefault();
                if (Check != null)
                {

                    
                    Check.IsDeleted = true;
                    Check.ModifiedBy = Obj.ModifiedBy;
                    Check.ModifiedDate = DateTime.Now;

                    dbContext.Entry(Check).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }

        }
    }
}
