//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Endmer.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_Dokuman
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Dokuman()
        {
            this.Tbl_DokumanResim = new HashSet<Tbl_DokumanResim>();
        }
    
        public int ID { get; set; }
        public string BASLIK { get; set; }
        public string ICERIK { get; set; }
        public Nullable<int> KONU { get; set; }
        public Nullable<bool> DURUM { get; set; }
    
        public virtual Tbl_DokumanKonu Tbl_DokumanKonu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_DokumanResim> Tbl_DokumanResim { get; set; }
    }
}
