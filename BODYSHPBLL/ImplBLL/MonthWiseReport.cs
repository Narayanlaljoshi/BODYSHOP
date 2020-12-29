using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BODYSHPBLL.ImplBLL
{
    public class MonthWiseReport
    {
        public string JobCardNo { get; set; }
        public string DateAndTime { get; set; }
        public string CustomerName { get; set; }
        public string Model { get; set; }
        public string RegistrationNo { get; set; }
        public Nullable<long> PhoneNo { get; set; }
        public string PromisedDate { get; set; }
        public string Service { get; set; }
        public long? No_of_Panel { get; set; }
        public long? Panel_Replaced { get; set; }
        public string ContractorName { get; set; }
        public string StatusName { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string PaymentMode { get; set; }
        public Nullable<long> NoOfGlass { get; set; }
        public string PhotoUrl { get; set; }
        public string CompanyName { get; set; }
    }
}
