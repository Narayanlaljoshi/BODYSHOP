using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BODYSHPBLL.ImplBLL
{
    public class JobcardDetails
    {
        public long JobCardId { get; set; }
        public string JobCardNo { get; set; }
        public Nullable<System.DateTime> DateAndTime { get; set; }
        public string CustomerName { get; set; }
        public Nullable<long> PhoneNo { get; set; }
        public string CustomerCategory { get; set; }
        public string PSFStatus { get; set; }
        public string RegistrationNo { get; set; }
        public string Model { get; set; }
        public string SA { get; set; }
        public string Technician { get; set; }
        public string Service { get; set; }
        public string PromisedDate { get; set; }
        public string RevPromisedDate { get; set; }
        public string ReadyDateTime { get; set; }
        public Nullable<long> InsuranceCompanyId { get; set; }
        public string PaymentMode { get; set; }
        public Nullable<long> DealerID { get; set; }
        public Nullable<long> AccountID { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<long> MogifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<long> ContractorId { get; set; }
        public Nullable<long> StatusID { get; set; }
        public Nullable<long> No_Of_Panel { get; set; }
        public Nullable<long> NoOfGlass { get; set; }
        public Nullable<long> PanelReplaced { get; set; }
        public Nullable<System.DateTime> StatusAddedDate { get; set; }
        public Nullable<System.DateTime> StatusModifiedDate { get; set; }
        public string StatusCode { get; set; }
        public string StatusName { get; set; }
        public Nullable<long> PhotoId { get; set; }
        public string PhotoUrl { get; set; }
        public string ContractorCode { get; set; }
        public string ContractorName { get; set; }
        public string CompanyName { get; set; }
    }
}
