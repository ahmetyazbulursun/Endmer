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
    
    public partial class Tbl_ZimmetAktar
    {
        public int ID { get; set; }
        public Nullable<int> VERENPERSONEL { get; set; }
        public Nullable<int> ALANPERSONEL { get; set; }
        public Nullable<int> URUN { get; set; }
        public Nullable<int> LOKASYON { get; set; }
        public string ADET { get; set; }
        public Nullable<System.DateTime> TARIH { get; set; }
        public Nullable<bool> DURUM { get; set; }
    
        public virtual Tbl_Konumlar Tbl_Konumlar { get; set; }
        public virtual Tbl_Personel Tbl_Personel { get; set; }
        public virtual Tbl_Personel Tbl_Personel1 { get; set; }
        public virtual Tbl_Urunler Tbl_Urunler { get; set; }
    }
}
