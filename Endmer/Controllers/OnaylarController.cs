using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Endmer.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace Endmer.Controllers
{

    [Authorize(Roles = "A")]

    public class OnaylarController : Controller
    {

        EndmerEntities db = new EndmerEntities();

        public ActionResult Index(int page = 1, string ara = "")
        {
            var value = from x in db.Tbl_ZimmetAktar.Where(x => x.DURUM == true) select x;

            if(!string.IsNullOrEmpty(ara))
            {
                value = db.Tbl_ZimmetAktar.Where(x => x.Tbl_Personel.AD.ToLower().Contains(ara) || x.Tbl_Personel.SOYAD.ToLower().Contains(ara) || x.Tbl_Personel1.AD.ToLower().Contains(ara) || x.Tbl_Personel1.SOYAD.ToLower().Contains(ara) || x.Tbl_Urunler.URUNADI.ToLower().Contains(ara) || x.Tbl_Urunler.MARKA.ToLower().Contains(ara) || x.Tbl_Urunler.MODEL.ToLower().Contains(ara) || x.Tbl_Urunler.BARKODNO.ToLower().Contains(ara) || x.Tbl_Konumlar.KONUM.ToLower().Contains(ara) && x.DURUM == true);
            }

            return View(value.ToList().ToPagedList(page, 50));
        }

        public ActionResult Onayla(Tbl_ZimmetAktar p, Tbl_Zimmetler z)
        {
            var value = db.Tbl_ZimmetAktar.Find(p.ID);
            var debit = db.Tbl_Zimmetler.Find(z.ID);

            value.ONAYDURUM = true;
            value.DURUM = false;
            debit.DURUM = false;
            debit.ONAYMESAJ = "Onaylandı";

            z.PERSONEL = value.ALANPERSONEL;
            z.ZIMMET = value.URUN;
            z.ADET = value.ADET;
            z.KONUM = value.LOKASYON;
            z.TARIH = DateTime.Now;
            z.DURUM = true;
            z.ONAYMESAJ = "Zimmetli";

            db.Tbl_Zimmetler.Add(z);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Reddet(Tbl_ZimmetAktar p, Tbl_Zimmetler z)
        {
            var value = db.Tbl_ZimmetAktar.Find(p.ID);
            var debit = db.Tbl_Zimmetler.Find(z.ID);

            value.ONAYDURUM = false;
            value.DURUM = false;
            debit.ONAYMESAJ = "Reddedildi";

            db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}