using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BODYSHPBLL.ImplBLL;
using BODYSHPDAL.ImplDAL;


namespace BODYSHP.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public SessionDataBLL LoginCheck(LoginBLL obj)
        {
           return LoginDAL.CheckLogin(obj);
        }
        
    }
}