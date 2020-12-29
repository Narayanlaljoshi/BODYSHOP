using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BODYSHPBLL.ImplBLL
{
    public class EmailListModel
    {
        public long Id { get; set; }
        public string PersonName { get; set; }
        public string Email_ID { get; set; }
        public Nullable<long> DealerId { get; set; }
        public Nullable<long> AccountId { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
