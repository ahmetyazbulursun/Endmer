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
    
    public partial class Tbl_Urunler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Urunler()
        {
            this.Tbl_Zimmetler = new HashSet<Tbl_Zimmetler>();
            this.Tbl_ZimmetAktar = new HashSet<Tbl_ZimmetAktar>();
        }
    
        public int ID { get; set; }
        public string URUNADI { get; set; }
        public string BARKODNO { get; set; }
        public Nullable<int> KATEGORI { get; set; }
        public string MARKA { get; set; }
        public string MODEL { get; set; }
        public string SERINO { get; set; }
        public string ACIKLAMA { get; set; }
        public Nullable<bool> DURUM { get; set; }
        public string RESIM { get; set; }
        public string ADET { get; set; }
        public Nullable<int> KONUM { get; set; }
        public string ARIZALIADET { get; set; }
    
        public virtual Tbl_Kategoriler Tbl_Kategoriler { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Zimmetler> Tbl_Zimmetler { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_ZimmetAktar> Tbl_ZimmetAktar { get; set; }
        public virtual Tbl_Konumlar Tbl_Konumlar { get; set; }
    }
}
