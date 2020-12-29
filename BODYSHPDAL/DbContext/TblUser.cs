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
    
    public partial class TblUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblUser()
        {
            this.TblJobCardHdrs = new HashSet<TblJobCardHdr>();
            this.tblBrands = new HashSet<tblBrand>();
        }
    
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<long> EmpId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual TblAccount TblAccount { get; set; }
        public virtual TblEmployee TblEmployee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblJobCardHdr> TblJobCardHdrs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblBrand> tblBrands { get; set; }
    }
}
