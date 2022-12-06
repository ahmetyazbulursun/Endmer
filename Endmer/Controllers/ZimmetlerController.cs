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

    public class ZimmetlerController : Controller
    {

        EndmerEntities db = new EndmerEntities();

        public ActionResult Index(int page = 1, string ara = "")
        {
            var value = from x in db.Tbl_Zimmetler.Where(x => x.DURUM == true && x.Tbl_Personel.DURUM == true && x.Tbl_Urunler.DURUM == true) select x;
        
            if(!string.IsNullOrEmpty(ara))
            {
                value = db.Tbl_Zimmetler.Where(x => x.Tbl_Urunler.URUNADI.ToLower().Contains(ara) || x.Tbl_Urunler.MARKA.ToLower().Contains(ara) || x.Tbl_Urunler.MODEL.ToLower().Contains(ara) || x.Tbl_Personel.AD.ToLower().Contains(ara) || x.Tbl_Personel.SOYAD.ToLower().Contains(ara) || x.ADET.ToLower().Contains(ara) || x.Tbl_Konumlar.KONUM.ToLower().Contains(ara) && x.DURUM == true);
            }

            return View(value.ToList().ToPagedList(page, 50));
        }


        [HttpGet]
        public ActionResult ZimmetVer()
        {
            List<SelectListItem> personnel = (from x in db.Tbl_Personel.Where(x => x.DURUM == true)
                                              select new SelectListItem
                                              {
                                                  Text = x.AD + " " + x.SOYAD,
                                                  Value = x.ID.ToString()
                                              }).ToList();
            ViewBag.Personnel = personnel;

            List<SelectListItem> product = (from x in db.Tbl_Urunler.Where(x => x.DURUM == true)
                                            select new SelectListItem
                                            {
                                                Text = x.URUNADI,
                                                Value = x.ID.ToString()
                                            }).ToList();
            ViewBag.Product = product;

            List<SelectListItem> location = (from x in db.Tbl_Konumlar.Where(x => x.DURUM == true)
                                             select new SelectListItem
                                             {
                                                 Text = x.KONUM,
                                                 Value = x.ID.ToString()
                                             }).ToList();
            ViewBag.Location = location;

            return View();
        }

        [HttpPost]
        public ActionResult ZimmetVer(Tbl_Zimmetler p)
        {
            var personnel = db.Tbl_Personel.Where(x => x.ID == p.Tbl_Personel.ID).FirstOrDefault();
            var product = db.Tbl_Urunler.Where(x => x.ID == p.Tbl_Urunler.ID).FirstOrDefault();
            var location = db.Tbl_Konumlar.Where(x => x.ID == p.Tbl_Konumlar.ID).FirstOrDefault();

            p.Tbl_Personel = personnel;
            p.Tbl_Urunler = product;
            p.Tbl_Konumlar = location;
            p.DURUM = true;
            p.ONAYMESAJ = "Zimmetli";

            db.Tbl_Zimmetler.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ZimmetSil(Tbl_Zimmetler p)
        {
            var value = db.Tbl_Zimmetler.Find(p.ID);

            value.DURUM = false;

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