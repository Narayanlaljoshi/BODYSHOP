//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BODYSHPDAL.DbContext
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblJobCardDtl
    {
        public long Id { get; set; }
        public Nullable<long> JobCardId { get; set; }
        public Nullable<long> StatusID { get; set; }
        public Nullable<long> No_Of_Panel { get; set; }
        public Nullable<long> PanelReplaced { get; set; }
        public Nullable<long> NoOfGlass { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<long> ContractorId { get; set; }
    
        public virtual TblStatu TblStatu { get; set; }
    }
}
