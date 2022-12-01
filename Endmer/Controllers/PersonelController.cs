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
    public class PersonelController : Controller
    {

        EndmerEntities db = new EndmerEntities();

        public ActionResult Index(int page = 1)
        {
            var value = db.Tbl_Personel.Where(x => x.DURUM == true).ToList().ToPagedList(page, 50);
            return View(value);
        }

        [HttpGet]
        public ActionResult PersonelEkle()
        {
            List<SelectListItem> departman = (from x in db.Tbl_Departmanlar.Where(x => x.DURUM == true).ToList()
                                              select new SelectListItem
                                              {
                                                  Text = x.DEPARTMAN,
                                                  Value = x.ID.ToString()
                                              }).ToList();
            ViewBag.Departman = departman;

            return View();
        }

        [HttpPost]
        public ActionResult PersonelEkle(Tbl_Personel p)
        {
            var departman = db.Tbl_Departmanlar.Where(x => x.ID == p.Tbl_Departmanlar.ID).FirstOrDefault();
         
            p.Tbl_Departmanlar = departman;
            p.DURUM = true;

            db.Tbl_Personel.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelSil(Tbl_Personel p)
        {
            var value = db.Tbl_Personel.Find(p.ID);

            value.DURUM = false;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult PersonelGuncelle(int id)
        {
            var value = db.Tbl_Personel.Find(id);

            List<SelectListItem> departman = (from x in db.Tbl_Departmanlar.Where(x => x.DURUM == true).ToList()
                                              select new SelectListItem
                                              {
                                                  Text = x.DEPARTMAN,
                                                  Value = x.ID.ToString()
                                              }).ToList();
            ViewBag.Departman = departman;

            return View("PersonelGuncelle", value);
        }

        [HttpPost]
        public ActionResult PersonelGuncelle(Tbl_Personel p)
        {
            var value = db.Tbl_Personel.Find(p.ID);

            var departman = db.Tbl_Departmanlar.Where(x => x.ID == p.Tbl_Departmanlar.ID).FirstOrDefault();

            value.Tbl_Departmanlar = departman;
            value.AD = p.AD;
            value.SOYAD = p.SOYAD;
            value.LOKASYON = p.LOKASYON;
            value.KULLANICIADI = p.KULLANICIADI;
            value.PAROLA = p.PAROLA;
            value.YETKI = p.YETKI;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelZimmet(int id)
        {
            var value = db.Tbl_Zimmetler.Where(x => x.PERSONEL == id && x.DURUM == true).ToList();
            return View(value);
        }

    }
}