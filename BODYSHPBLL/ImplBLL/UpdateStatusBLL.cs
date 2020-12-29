using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BODYSHPBLL.ImplBLL
{
    public class UpdateStatusBLL
    {
        public long JobCardId { get; set; }
        public long StatusID { get; set; }
        public long NumberOfPanel { get; set; }
        public long PanelReplaced { get; set; }
        public long ModifiedBy { get; set; }
        public Nullable<long> Contractor_Id { get; set; }
        public long Glass { get; set; }
        public string PaymentMode { get; set; }
        public long? InsCompId { get; set; }
        public string PhotoUrl { get; set; }

    }
}
