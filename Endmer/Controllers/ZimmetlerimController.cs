using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Endmer.Models.Entity;
using Newtonsoft.Json.Linq;
using PagedList;
using PagedList.Mvc;

namespace Endmer.Controllers
{

    [Authorize(Roles = "P")]

    public class ZimmetlerimController : Controller
    {

        EndmerEntities db = new EndmerEntities();

        public ActionResult Index(int page = 1)
        {
            int userID = Convert.ToInt32(Session["ID"]);
            var value = db.Tbl_Zimmetler.Where(x => x.PERSONEL == userID && x.DURUM == true).ToList().ToPagedList(page, 50);
            return View(value);
        }

        [HttpGet]
        public ActionResult Aktar(int id)
        {
            List<SelectListItem> personnel = (from x in db.Tbl_Personel.Where(x => x.DURUM == true)
                                              select new SelectListItem
                                              {
                                                  Text = x.AD + " " + x.SOYAD,
                                                  Value = x.ID.ToString()
                                              }).ToList();
            ViewBag.Personnel = personnel;

            List<SelectListItem> location = (from x in db.Tbl_Konumlar.Where(x=>x.DURUM == true)
                                             select new SelectListItem
                                             {
                                                 Text = x.KONUM,
                                                 Value = x.ID.ToString()
                                             }).ToList();
            ViewBag.Location = location;

            return View();
        }

        [HttpPost]
        public ActionResult Aktar(Tbl_ZimmetAktar p, Tbl_Zimmetler z)
        {
            var giver = db.Tbl_Zimmetler.Find(z.ID);
            var personnel = db.Tbl_Personel.Where(x => x.ID == p.Tbl_Personel.ID).FirstOrDefault();
            var product = db.Tbl_Zimmetler.Find(z.ID);
            var location = db.Tbl_Konumlar.Where(x=>x.ID == p.Tbl_Konumlar.ID).FirstOrDefault();
            var debit = db.Tbl_Zimmetler.Find(z.ID);

            p.Tbl_Personel = giver.Tbl_Personel;
            p.Tbl_Personel1 = personnel;
            p.URUN = product.ZIMMET;
            p.Tbl_Konumlar = location;
            p.DURUM = true;
            p.TARIH = DateTime.Now;
            debit.ONAYMESAJ = "Onay Bekliyor";

            db.Tbl_ZimmetAktar.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            var value = db.Tbl_Zimmetler.Find(id);
            return View("Guncelle", value);
        }

        [HttpPost]
        public ActionResult Guncelle(Tbl_Zimmetler p)
        {
            var value = db.Tbl_Zimmetler.Find(p.ID);

            value.ADET = p.ADET;
            value.ARIZALIADET = p.ARIZALIADET;

            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}