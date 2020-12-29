using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BODYSHPBLL.ImplBLL
{
    public class CompleteDetailsModel
    {
        public long JobCardId { get; set; }
        public string CustomerCategory { get; set; }
        public string CustomerName { get; set; }
        public Nullable<System.DateTime> DateAndTime { get; set; }
        public string JobCardNo { get; set; }
        public string Model { get; set; }
        public Nullable<long> PhoneNo { get; set; }
        public string PromisedDate { get; set; }
        public string SA { get; set; }
        public string Service { get; set; }
        public string RegistrationNo { get; set; }
        public string PSFStatus { get; set; }
        public string Technician { get; set; }
        public Nullable<long> DealerID { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public long Id { get; set; }
        public Nullable<long> No_Of_Panel { get; set; }
        public Nullable<long> PanelReplaced { get; set; }
        public string Status { get; set; }
        public Nullable<long> StatusID { get; set; }
        public string ModifiedDate_Formatted { get; set; }
        public Nullable<long> Contractor_Id { get; set; }
        public string ContractorName { get; set; }
        public Nullable<long> InsCompId { get; set; }
        public string PaymentMode { get; set; }
        public Nullable<long> Glass { get; set; }
        public string PhotoUrl { get; set; }
    }
}
