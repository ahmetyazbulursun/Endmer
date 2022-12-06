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

    [Authorize(Roles = "A")]

    public class AraclarController : Controller
    {

        EndmerEntities db = new EndmerEntities();

        public ActionResult Index(int page = 1, string ara = "")
        {
            var value = from x in db.Tbl_Araclar.Where(x => x.DURUM == true) select x;

            if (!string.IsNullOrEmpty(ara))
            {
                value = value.Where(x => x.MARKA.ToLower().Contains(ara) || x.PLAKA.ToLower().Contains(ara) || x.KM.ToLower().Contains(ara) || x.Tbl_Konumlar.KONUM.ToLower().Contains(ara) || x.Tbl_Personel.AD.ToLower().Contains(ara) || x.Tbl_Personel.SOYAD.ToLower().Contains(ara) && x.DURUM == true);
            }

            return View(value.ToList().ToPagedList(page, 50));
        }

        [HttpGet]
        public ActionResult AracEkle()
        {
            List<SelectListItem> profit = (from x in db.Tbl_Personel.Where(x => x.DURUM == true)
                                           select new SelectListItem
                                           {
                                               Text = x.AD + " " + x.SOYAD,
                                               Value = x.ID.ToString()
                                           }).ToList();
            ViewBag.Profit = profit;

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
        public ActionResult AracEkle(Tbl_Araclar p)
        {
            var profit = db.Tbl_Personel.Where(x => x.ID == p.Tbl_Personel.ID).FirstOrDefault();
            var location = db.Tbl_Konumlar.Where(x => x.ID == p.Tbl_Konumlar.ID).FirstOrDefault();

            p.Tbl_Personel = profit;
            p.Tbl_Konumlar = location;
            p.RESIM = "/template/default-img.jpg";
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

            List<SelectListItem> location = (from x in db.Tbl_Konumlar.Where(x => x.DURUM == true)
                                             select new SelectListItem
                                             {
                                                 Text = x.KONUM,
                                                 Value = x.ID.ToString()
                                             }).ToList();
            ViewBag.Location = location;

            return View("AracGuncelle", value);
        }

        [HttpPost]
        public ActionResult AracGuncelle(Tbl_Araclar p, Tbl_AracKayit k, HttpPostedFileBase RESIM)
        {
            var value = db.Tbl_Araclar.Find(p.ID);

            try
            {
                if (RESIM.ContentLength > 0)
                {
                    string filePath = Path.Combine(Server.MapPath("~/Images/Vehicles"), Path.GetFileName(RESIM.FileName));
                    RESIM.SaveAs(filePath);

                    string fileName = Path.GetFileName(Request.Files[0].FileName);
                    string extension = Path.GetExtension(Request.Files[0].FileName);
                    string path = "~/Images/Vehicles/" + fileName;
                    Request.Files[0].SaveAs(Server.MapPath(path));
                    value.RESIM = "/Images/Vehicles/" + fileName;
                }
            }
            catch (Exception)
            {
                value.RESIM = value.RESIM;
            }

            var vehicle = db.Tbl_AracKayit.Find(p.ID);
            var location = db.Tbl_Konumlar.Where(x => x.ID == p.Tbl_Konumlar.ID).FirstOrDefault();

            value.MARKA = p.MARKA;
            value.PLAKA = p.PLAKA;
            value.KM = p.KM;
            value.Tbl_Konumlar = location;

            k.ARAC = value.ID;
            k.KULLANICI = value.TESLIMEDEN;
            k.KM = p.KM;
            k.Tbl_Konumlar = location;
            k.TARIH = DateTime.Now;
            k.DURUM = true;

            db.Tbl_AracKayit.Add(k);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult YeniKullanici(int id)
        {
            var value = db.Tbl_Araclar.Find(id);

            List<SelectListItem> teslimAlan = (from x in db.Tbl_Personel.Where(x => x.DURUM == true).ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.AD + " " + x.SOYAD,
                                                   Value = x.ID.ToString()
                                               }).ToList();
            ViewBag.TeslimAlan = teslimAlan;

            return View(value);
        }

        [HttpPost]
        public ActionResult YeniKullanici(Tbl_Araclar p, Tbl_AracKayit k)
        {
            return RedirectToAction("Index");
        }

        public ActionResult Kayitlar(int id)
        {
            var value = db.Tbl_AracKayit.Where(x => x.ARAC == id && x.DURUM == true).ToList();
            return View(value);
        }



    }
}