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
    using System.ComponentModel.DataAnnotations;

    public partial class Tbl_Kategoriler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Kategoriler()
        {
            this.Tbl_Urunler = new HashSet<Tbl_Urunler>();
        }
    
        public int ID { get; set; }
        [MinLength(3, ErrorMessage = "En az 3 karakter girilmelidir!"), MaxLength(100, ErrorMessage = "En fazla 100 karakter girilebilir!")]
        public string KATEGORIADI { get; set; }
        public Nullable<bool> DURUM { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Urunler> Tbl_Urunler { get; set; }
    }
}
