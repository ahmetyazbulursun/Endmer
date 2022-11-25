using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using Endmer.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace Endmer.Controllers
{
    public class AraclarController : Controller
    {

        EndmerEntities db = new EndmerEntities();

        public ActionResult Index(int page = 1)
        {
            var value = db.Tbl_Araclar.Where(x => x.DURUM == true).ToList().ToPagedList(page, 50);
            return View(value);
        }

        [HttpGet]
        public ActionResult AracEkle()
        {
            List<SelectListItem> teslimEden = (from x in db.Tbl_Personel.Where(x => x.DURUM == true).ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.AD + " " + x.SOYAD,
                                                   Value = x.ID.ToString()
                                               }).ToList();
            ViewBag.TeslimEden = teslimEden;

            List<SelectListItem> teslimAlan = (from x in db.Tbl_Personel.Where(x => x.DURUM == true).ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.AD + " " + x.SOYAD,
                                                   Value = x.ID.ToString()
                                               }).ToList();
            ViewBag.TeslimAlan = teslimAlan;

            return View();
        }

        [HttpPost]
        public ActionResult AracEkle(Tbl_Araclar p, HttpPostedFileBase RESIM)
        {
            if (RESIM.ContentLength > 0)
            {
                string filePath = Path.Combine(Server.MapPath("~/Images/Vehicles"), Path.GetFileName(RESIM.FileName));
                RESIM.SaveAs(filePath);

                string fileName = Path.GetFileName(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/Vehicles/" + fileName;
                Request.Files[0].SaveAs(Server.MapPath(path));
                p.RESIM = "/Images/Vehicles/" + fileName;
            }

            var teslimEden = db.Tbl_Personel.Where(x => x.ID == p.Tbl_Personel.ID).FirstOrDefault();
            var teslimAlan = db.Tbl_Personel.Where(x => x.ID == p.Tbl_Personel1.ID).FirstOrDefault();

            p.Tbl_Personel = teslimEden;
            p.Tbl_Personel1 = teslimAlan;
            p.DURUM = true;

            db.Tbl_Araclar.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AracDetay(int id)
        {
            var value = db.Tbl_Araclar.Where(x => x.ID == id).ToList();
            return View(value);
        }

        public ActionResult AracSil(Tbl_Araclar p)
        {
            var value = db.Tbl_Araclar.Find(p.ID);
            value.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AracGuncelle(int id)
        {
            var value = db.Tbl_Araclar.Find(id);
            return View("AracGuncelle", value);
        }

        [HttpPost]
        public ActionResult AracGuncelle(Tbl_Araclar p)
        {
            var value = db.Tbl_Araclar.Find(p.ID);

            value.MARKA = p.MARKA;
            value.PLAKA = p.PLAKA;
            value.LOKASYON = p.LOKASYON;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult YeniKullanici(int id)
        {
            var value = db.Tbl_Araclar.Find(id);

            List<SelectListItem> teslimEden = (from x in db.Tbl_Personel.Where(x => x.DURUM == true).ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.AD + " " + x.SOYAD,
                                                   Value = x.ID.ToString()
                                               }).ToList();
            ViewBag.TeslimEden = teslimEden;

            List<SelectListItem> teslimAlan = (from x in db.Tbl_Personel.Where(x => x.DURUM == true).ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.AD + " " + x.SOYAD,
                                                   Value = x.ID.ToString()
                                               }).ToList();
            ViewBag.TeslimAlan = teslimAlan;

            return View("YeniKullanici", value);
        }

        [HttpPost]
        public ActionResult YeniKullanici(Tbl_Araclar p, Tbl_AracKayit k)
        {
            var value = db.Tbl_Araclar.Find(p.ID);
            var history = k;

            var vehicle = db.Tbl_Araclar.Find(p.ID);

            var teslimEden = db.Tbl_Personel.Where(x => x.ID == p.Tbl_Personel.ID).FirstOrDefault();
            var teslimAlan = db.Tbl_Personel.Where(x => x.ID == p.Tbl_Personel1.ID).FirstOrDefault();

            value.Tbl_Personel = teslimEden;
            value.Tbl_Personel1 = teslimAlan;
            value.KM = p.KM;
            value.LOKASYON = p.LOKASYON;

            history.ARAC = vehicle.ID;
            history.Tbl_Personel = teslimEden;
            history.Tbl_Personel1 = teslimAlan;
            history.TARIH = DateTime.Now;
            history.DURUM = true;

            db.Tbl_AracKayit.Add(k);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Kayitlar(int id)
        {
            var value = db.Tbl_AracKayit.Where(x => x.ARAC == id).ToList();
            return View(value);
        }



    }
}