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

    public partial class Tbl_Personel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Personel()
        {
            this.Tbl_AracKayit = new HashSet<Tbl_AracKayit>();
            this.Tbl_AracKayit1 = new HashSet<Tbl_AracKayit>();
            this.Tbl_Zimmetler = new HashSet<Tbl_Zimmetler>();
            this.Tbl_ZimmetAktar = new HashSet<Tbl_ZimmetAktar>();
            this.Tbl_ZimmetAktar1 = new HashSet<Tbl_ZimmetAktar>();
            this.Tbl_Araclar = new HashSet<Tbl_Araclar>();
        }

        public int ID { get; set; }
        [MinLength(3, ErrorMessage = "En az 3 karakter girilmelidir!"), MaxLength(30, ErrorMessage = "En fazla 30 karakter girilebilir!")]
        public string AD { get; set; }
        [MinLength(3, ErrorMessage = "En az 3 karakter girilmelidir!"), MaxLength(30, ErrorMessage = "En fazla 30 karakter girilebilir!")]
        public string SOYAD { get; set; }
        public Nullable<int> DEPARTMAN { get; set; }
        public Nullable<bool> DURUM { get; set; }
        [MinLength(3, ErrorMessage = "En az 3 karakter girilmelidir!"), MaxLength(50, ErrorMessage = "En fazla 50 karakter girilebilir!")]
        public string KULLANICIADI { get; set; }
        [MinLength(3, ErrorMessage = "En az 3 karakter girilmelidir!"), MaxLength(10, ErrorMessage = "En fazla 10 karakter girilebilir!")]
        public string PAROLA { get; set; }
        public string YETKI { get; set; }
        public Nullable<int> LOKASYON { get; set; }

        public virtual Tbl_Departmanlar Tbl_Departmanlar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_AracKayit> Tbl_AracKayit { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_AracKayit> Tbl_AracKayit1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Zimmetler> Tbl_Zimmetler { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_ZimmetAktar> Tbl_ZimmetAktar { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_ZimmetAktar> Tbl_ZimmetAktar1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_Araclar> Tbl_Araclar { get; set; }
        public virtual Tbl_Konumlar Tbl_Konumlar { get; set; }
    }
}
