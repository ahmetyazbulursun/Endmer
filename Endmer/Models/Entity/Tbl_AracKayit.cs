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

    public partial class Tbl_AracKayit
    {
        public int ID { get; set; }
        public Nullable<int> ARAC { get; set; }
        public Nullable<int> TESLIMEDEN { get; set; }
        public Nullable<int> TESLIMALAN { get; set; }
        public Nullable<System.DateTime> TARIH { get; set; }
        public Nullable<bool> DURUM { get; set; }
        [MinLength(3, ErrorMessage = "En az 3 karakter girilmelidir!"), MaxLength(50, ErrorMessage = "En fazla 50 karakter girilebilir!")]
        public string KM { get; set; }
        public Nullable<int> LOKASYON { get; set; }
    
        public virtual Tbl_Araclar Tbl_Araclar { get; set; }
        public virtual Tbl_Personel Tbl_Personel { get; set; }
        public virtual Tbl_Personel Tbl_Personel1 { get; set; }
        public virtual Tbl_Konumlar Tbl_Konumlar { get; set; }
    }
}
