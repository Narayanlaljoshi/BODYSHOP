using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BODYSHPBLL.ImplBLL;
using BODYSHPDAL.DbContext;

namespace BODYSHPDAL.ImplDAL
{
    public class LoginDAL
    {
        public static SessionDataBLL CheckLogin(LoginBLL obj)
        {
           
            using(var dbContext=new BSSDBEntities())
            {
                var Check = dbContext.TblUsers.Where(x => x.UserName == obj.UserName && x.Password == obj.Password &&x.IsActive==true).FirstOrDefault();
                if (Check != null)
                {
                    var DesignationID = dbContext.TblEmployees.Where(x => x.EmpId == Check.EmpId).Select(x=>x.DesignationID).FirstOrDefault();

                    var Details = dbContext.TblEmployees.Where(x => x.EmpId == Check.EmpId).FirstOrDefault();
                    SessionDataBLL SessionData = new SessionDataBLL
                    {
                        UserId = Check.UserID,
                        UserName=Check.UserName,
                        AccountId= Details.AccountID.Value,
                        EmployeeId=Check.EmpId.Value,
                        DesignationID=Convert.ToInt64(DesignationID),
                        DealerID=Details.DealerID.Value,
                        
                    };
                    return SessionData;
                }


                else
                    return null;
            }
        }

    }
}
