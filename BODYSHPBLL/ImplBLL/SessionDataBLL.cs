using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BODYSHPBLL.ImplBLL
{
    public class SessionDataBLL
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public long? AccountId { get; set; }
        public long EmployeeId { get; set; }
        public long DesignationID { get; set; }
        public long? DealerID { get; set; }
    }
    public class FilterData
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public long? AccountId { get; set; }
        public long EmployeeId { get; set; }
        public long DesignationID { get; set; }
        public long? DealerID { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
    }
}
