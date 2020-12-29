using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BODYSHPBLL.ImplBLL
{
    public class InsuranceCmpList
    {
        public long InsuranceCompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<long> DealerId { get; set; }
        public Nullable<long> AccountId { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
