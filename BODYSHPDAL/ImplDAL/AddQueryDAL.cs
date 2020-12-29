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
    public class AddQueryDAL
    {
        public static void AddQuery(AddQueryBLL obj)
        {
            string JobCardNo = obj.JobCardNo;
            using (var dbcontext=new BSSDBEntities())
            {
                var Check = dbcontext.TblJobCardHdrs.Where(x => x.JobCardNo == JobCardNo).FirstOrDefault();
                if (Check ==null)
                {
                    TblJobCardHdr TR = new TblJobCardHdr
                    {
                        JobCardNo = obj.JobCardNo,
                        DateAndTime = DateTime.Now,
                        CustomerName=obj.CustomerName,
                        PhoneNo=obj.PhoneNo,
                        CustomerCategory=obj.CustomerCategory,
                        PSFStatus=obj.PSFStatus,
                        RegistrationNo=obj.RegistrationNo,
                        Model=obj.Model,
                        SA=obj.SA,
                        Technician=obj.Technician,
                        Service=obj.Service,
                        PromisedDate=obj.PromisedDate
                    };

                    dbcontext.Entry(TR).State = EntityState.Added;
                    dbcontext.SaveChanges();

                    var JobCardId = dbcontext.TblJobCardHdrs.Where(x => x.JobCardNo == JobCardNo).Select(s => s.JobCardId).FirstOrDefault();
                   
                        TblJobCardDtl tb = new TblJobCardDtl
                        {
                            JobCardId= JobCardId,
                            No_Of_Panel=obj.NoOfPanel,
                            PanelReplaced=obj.PanelReplaced,
                            //Status=obj.Status
                        };
                    dbcontext.Entry(tb).State = EntityState.Added;
                    dbcontext.SaveChanges();

                }
            }
        }
    }
}
