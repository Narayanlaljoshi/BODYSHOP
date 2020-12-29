using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BODYSHPBLL.ImplBLL
{
    public class ContractorModel
    {
        public long Contractor_Id { get; set; }
        public string ContractorCode { get; set; }
        public string ContractorName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Nullable<long> DealerID { get; set; }
        public Nullable<long> AccountID { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        
    }
}
