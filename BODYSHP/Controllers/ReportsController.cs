using BODYSHPBLL.ImplBLL;
using BODYSHPDAL.ImplDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BODYSHP.Controllers
{
    public class ReportsController : ApiController
    {
        [HttpPost]
        public List<ModelAndFuelTypeStatus> MonthAndFuelTypeStatus(FilterData Obj)
        {
            return ReportsDAL.MonthAndFuelTypeStatus(Obj);
        }
        public List<PanelWiseStatus> PanelWiseStatus(FilterData Obj)
        {
            return ReportsDAL.PanelWiseStatus(Obj);
        }
    }
}
