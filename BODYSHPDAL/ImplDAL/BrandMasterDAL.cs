using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BODYSHPDAL.DbContext;

namespace BODYSHPDAL.ImplDAL
{
    public class BrandMasterDAL
    {
        public static List<spGetBrandList_Result> BrandList()
        {
            List<spGetBrandList_Result> BrandListData = null;
            using (var dbContext=new BSSDBEntities())
            {
                var ReqData = dbContext.spGetBrandList();
                BrandListData= ReqData.ToList();
            }
            return BrandListData;
        }

        public static void Post(string BrandName, long UserId)
        {
            
            using (var dbContext = new BSSDBEntities())
            {
                var Check = dbContext.tblBrands.Where(x=>x.BrandName==BrandName).FirstOrDefault();

                if (Check==null)
                {
                    tblBrand tb = new tblBrand
                    {
                        BrandName = BrandName,
                        IsDeleted = false,
                        CreatedBy= UserId,
                        CreationDate =DateTime.Now
                    };
                    dbContext.Entry(tb).State = System.Data.Entity.EntityState.Added;
                    dbContext.SaveChanges();
                }



            }
           
        }

        public static void Update(string BrandName, long BrandId)
        {
            using (var dbContext = new BSSDBEntities())
            {
                var Check = dbContext.tblBrands.Where(x => x.BrandId == BrandId).FirstOrDefault();

                if (Check != null)
                {
                    Check.BrandName = BrandName;
                    dbContext.Entry(Check).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                }



            }
        }

        public static void Delete(long BrandId)
        {
            using (var dbContext = new BSSDBEntities())
            {
                var Check = dbContext.tblBrands.Where(x => x.BrandId == BrandId).FirstOrDefault();

                if (Check != null)
                {
                    Check.IsDeleted = true;
                    dbContext.Entry(Check).State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                    
                }
                
            }

        }
    }

   
}
