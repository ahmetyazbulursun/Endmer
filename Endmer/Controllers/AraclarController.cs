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
using Newtonsoft.Json.Linq;
using PagedList;
using PagedList.Mvc;

namespace Endmer.Controllers
{

    [Authorize(Roles = "A")]

    public class AraclarController : Controller
    {

        EndmerEntities db = new EndmerEntities();

        public ActionResult Index(int page = 1)
        {
            if (Session["ID"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Logout", "Login");
            }

            var value = db.Tbl_Araclar.Where(x => x.DURUM == true).ToList().ToPagedList(page, 50);

            return View(value);
        }

        [HttpGet]
        public ActionResult AracEkle()
        {
            List<SelectListItem> debit = (from x in db.Tbl_Personel.Where(x => x.DURUM == true).OrderBy(x => x.AD + x.SOYAD)
                                          select new SelectListItem
                                          {
                                              Text = x.AD + " " + x.SOYAD,
                                              Value = x.ID.ToString()
                                          }).ToList();
            ViewBag.Debit = debit;

            List<SelectListItem> location = (from x in db.Tbl_Konumlar.Where(x => x.DURUM == true).OrderBy(x => x.KONUM)
                                             select new SelectListItem
                                             {
                                                 Text = x.KONUM,
                                                 Value = x.ID.ToString()
                                             }).ToList();
            ViewBag.Location = location;

            return View();
        }

        [HttpPost]
        public ActionResult AracEkle(Tbl_Araclar p, HttpPostedFileBase RESIM)
        {
            if (!ModelState.IsValid)
            {
                List<SelectListItem> debitt = (from x in db.Tbl_Personel.Where(x => x.DURUM == true).OrderBy(x => x.AD + x.SOYAD)
                                               select new SelectListItem
                                               {
                                                   Text = x.AD + " " + x.SOYAD,
                                                   Value = x.ID.ToString()
                                               }).ToList();
                ViewBag.Debit = debitt;

                List<SelectListItem> locationn = (from x in db.Tbl_Konumlar.Where(x => x.DURUM == true).OrderBy(x => x.KONUM)
                                                  select new SelectListItem
                                                  {
                                                      Text = x.KONUM,
                                                      Value = x.ID.ToString()
                                                  }).ToList();
                ViewBag.Location = locationn;

                return View("AracEkle");
            }

            string vehiclesFile = "~/Images/Vehicles";
            bool exists = System.IO.Directory.Exists(Server.MapPath(vehiclesFile));

            if (!exists)
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(vehiclesFile));
            }

            try
            {
                if (RESIM.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(Request.Files[0].FileName);
                    string extension = Path.GetExtension(Request.Files[0].FileName);
                    string date = Convert.ToString(DateTime.Now.ToLongDateString());
                    string time = Convert.ToString(DateTime.Now.ToLongTimeString()).Replace(":", "-");
                    string dateTime = date + "-" + time + "-";
                    string path = "~/Images/Vehicles/" + dateTime + fileName;
                    Request.Files[0].SaveAs(Server.MapPath(path));
                    p.RESIM = "/Images/Vehicles/" + dateTime + fileName;
                }
            }
            catch (Exception)
            {
                p.RESIM = "/template/default-img.jpg";
            }

            var location = db.Tbl_Konumlar.Where(x => x.ID == p.Tbl_Konumlar.ID).FirstOrDefault();
            var debit = db.Tbl_Personel.Where(x => x.ID == p.Tbl_Personel.ID).FirstOrDefault();

            p.Tbl_Konumlar = location;
            p.Tbl_Personel = debit;
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

            List<SelectListItem> location = (from x in db.Tbl_Konumlar.Where(x => x.DURUM == true).OrderBy(x => x.KONUM)
                                             select new SelectListItem
                                             {
                                                 Text = x.KONUM,
                                                 Value = x.ID.ToString()
                                             }).ToList();
            ViewBag.Location = location;

            return View("AracGuncelle", value);
        }

        [HttpPost]
        public ActionResult AracGuncelle(Tbl_Araclar p, Tbl_BakimKayit k, HttpPostedFileBase RESIM)
        {
            if (!ModelState.IsValid)
            {
                List<SelectListItem> locationn = (from x in db.Tbl_Konumlar.Where(x => x.DURUM == true).OrderBy(x => x.KONUM)
                                                  select new SelectListItem
                                                  {
                                                      Text = x.KONUM,
                                                      Value = x.ID.ToString()
                                                  }).ToList();
                ViewBag.Location = locationn;

                return View("AracGuncelle");
            }

            var value = db.Tbl_Araclar.Find(p.ID);

            try
            {
                if (RESIM.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(Request.Files[0].FileName);
                    string extension = Path.GetExtension(Request.Files[0].FileName);
                    string date = Convert.ToString(DateTime.Now.ToLongDateString());
                    string time = Convert.ToString(DateTime.Now.ToLongTimeString()).Replace(":", "-");
                    string dateTime = date + "-" + time + "-";
                    string path = "~/Images/Vehicles/" + dateTime + fileName;
                    string filePath = "/Images/Vehicles/" + dateTime + fileName;
                    Request.Files[0].SaveAs(Server.MapPath(path));
                    value.RESIM = filePath;
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

            //k.ARAC = value.ID;
            //k.TARIH = p.BAKIMZAMANI;
            //k.DURUM = true;

            //db.Tbl_BakimKayit.Add(k);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult YeniKullanici(int id)
        {
            var value = db.Tbl_Araclar.Find(id);

            List<SelectListItem> delivery = (from x in db.Tbl_Personel.Where(x => x.DURUM == true).ToList().OrderBy(x => x.AD + x.SOYAD)
                                             select new SelectListItem
                                             {
                                                 Text = x.AD + " " + x.SOYAD,
                                                 Value = x.ID.ToString()
                                             }).ToList();
            ViewBag.Delivery = delivery;

            List<SelectListItem> location = (from x in db.Tbl_Konumlar.Where(x => x.DURUM == true).ToList().OrderBy(x => x.KONUM)
                                             select new SelectListItem
                                             {
                                                 Text = x.KONUM,
                                                 Value = x.ID.ToString()
                                             }).ToList();
            ViewBag.Location = location;

            return View(value);
        }

        [HttpPost]
        public ActionResult YeniKullanici(Tbl_Araclar p, Tbl_AracKayit k)
        {
            if (!ModelState.IsValid)
            {
                List<SelectListItem> deliveryy = (from x in db.Tbl_Personel.Where(x => x.DURUM == true).ToList().OrderBy(x => x.AD + x.SOYAD)
                                                  select new SelectListItem
                                                  {
                                                      Text = x.AD + " " + x.SOYAD,
                                                      Value = x.ID.ToString()
                                                  }).ToList();
                ViewBag.Delivery = deliveryy;

                List<SelectListItem> locationn = (from x in db.Tbl_Konumlar.Where(x => x.DURUM == true).ToList().OrderBy(x => x.KONUM)
                                                  select new SelectListItem
                                                  {
                                                      Text = x.KONUM,
                                                      Value = x.ID.ToString()
                                                  }).ToList();
                ViewBag.Location = locationn;

                return View("YeniKullanici");
            }

            var vehicle = db.Tbl_Araclar.Find(p.ID);
            var newDebit = db.Tbl_Personel.Where(x => x.ID == p.Tbl_Personel.ID).FirstOrDefault();
            var newLocation = db.Tbl_Konumlar.Where(x => x.ID == p.Tbl_Konumlar.ID).FirstOrDefault();

            k.ARAC = vehicle.ID;
            k.Tbl_Personel = vehicle.Tbl_Personel;
            k.Tbl_Personel1 = newDebit;
            k.KM = p.KM;
            k.Tbl_Konumlar = newLocation;
            k.TARIH = DateTime.Now;
            k.DURUM = true;

            vehicle.Tbl_Personel = newDebit;
            vehicle.Tbl_Konumlar = newLocation;
            vehicle.KM = p.KM;

            db.Tbl_AracKayit.Add(k);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Kayitlar(int id)
        {
            var value = db.Tbl_AracKayit.Where(x => x.ARAC == id && x.DURUM == true).ToList();
            return View(value);
        }

        public ActionResult BakimZamani(int id)
        {
            Session["VEHICLEID"] = id;
            var value = db.Tbl_BakimKayit.Where(x => x.ARAC == id && x.DURUM == true).ToList();
            return View(value);
        }

        [HttpGet]
        public ActionResult BakimEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BakimEkle(Tbl_BakimKayit p, Tbl_Araclar k)
        {
            var value = db.Tbl_Araclar.Find(k.ID);

            p.ARAC = value.ID;
            p.TARIH = p.TARIH;
            p.BAKIMDURUM = "Yapılmadı";
            p.DURUM = true;

            db.Tbl_BakimKayit.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Yapildi(Tbl_BakimKayit p)
        {
            var value = db.Tbl_BakimKayit.Find(p.ID);

            value.BAKIMDURUM = "Yapıldı";

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult BakimSil(Tbl_BakimKayit p)
        {
            var value = db.Tbl_BakimKayit.Find(p.ID);

            value.DURUM = false;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //public ActionResult Yazdir()
        //{
        //    var value = db.Tbl_Araclar.Where(x => x.DURUM == true && x.Tbl_Personel.DURUM == true && x.Tbl_Konumlar.DURUM == true).ToList();
        //    return View(value);
        //}

        public ActionResult Yazdir()
        {
            var value = db.Tbl_Araclar.Where(x => x.DURUM == true && x.Tbl_Personel.DURUM == true && x.Tbl_Konumlar.DURUM == true).ToList();
            return View(value);
        }

        public ActionResult GecmisYazdir(int id)
        {
            var value = db.Tbl_AracKayit.Where(x => x.ARAC == id && x.DURUM == true && x.Tbl_Personel.DURUM == true && x.Tbl_Konumlar.DURUM == true).ToList();
            return View(value);
        }


    }
}