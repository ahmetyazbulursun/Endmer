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
    
    public partial class Tbl_Araclar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Araclar()
        {
            this.Tbl_AracKayit = new HashSet<Tbl_AracKayit>();
        }
    
        public int ID { get; set; }
        public string PLAKA { get; set; }
        public Nullable<int> LOKASYON { get; set; }
        public Nullable<bool> DURUM { get; set; }
        public string RESIM { get; set; }
        public string KM { get; set; }
        public string MARKA { get; set; }
        public Nullable<int> KULLANICI { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_AracKayit> Tbl_AracKayit { get; set; }
        public virtual Tbl_Konumlar Tbl_Konumlar { get; set; }
        public virtual Tbl_Personel Tbl_Personel { get; set; }
    }
}
