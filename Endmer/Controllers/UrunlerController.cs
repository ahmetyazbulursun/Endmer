using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Endmer.Models.Entity;
using Newtonsoft.Json.Linq;
using PagedList;
using PagedList.Mvc;

namespace Endmer.Controllers
{

    [Authorize(Roles = "A")]

    public class UrunlerController : Controller
    {

        EndmerEntities db = new EndmerEntities();

        public ActionResult Index(int page = 1)
        {
            if (Session["ID"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Logout", "Login");
            }

            var value = db.Tbl_Urunler.Where(x => x.DURUM == true).ToList().ToPagedList(page, 50);
            return View(value);
        }

        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> category = (from x in db.Tbl_Kategoriler.Where(x => x.DURUM == true).ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.KATEGORIADI,
                                                 Value = x.ID.ToString()
                                             }).ToList();
            ViewBag.Category = category;

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
        public ActionResult UrunEkle(Tbl_Urunler p, HttpPostedFileBase RESIM)
        {
            string productsFile = "~/Images/Products";
            bool exists = System.IO.Directory.Exists(Server.MapPath(productsFile));

            if (!exists)
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(productsFile));
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
                    string path = "~/Images/Products/" + dateTime + fileName;
                    Request.Files[0].SaveAs(Server.MapPath(path));
                    p.RESIM = "/Images/Products/" + dateTime + fileName;
                }
            }
            catch (Exception)
            {
                p.RESIM = "/template/default-img.jpg";
            }

            var category = db.Tbl_Kategoriler.Where(x => x.ID == p.Tbl_Kategoriler.ID).FirstOrDefault();
            var location = db.Tbl_Konumlar.Where(x => x.ID == p.Tbl_Konumlar.ID).FirstOrDefault();

            p.Tbl_Kategoriler = category;
            p.Tbl_Konumlar = location;
            p.DURUM = true;

            db.Tbl_Urunler.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UrunGuncelle(int id)
        {
            var value = db.Tbl_Urunler.Find(id);

            List<SelectListItem> category = (from x in db.Tbl_Kategoriler.Where(x => x.DURUM == true)
                                             select new SelectListItem
                                             {
                                                 Text = x.KATEGORIADI,
                                                 Value = x.ID.ToString()
                                             }).ToList();
            ViewBag.Category = category;

            List<SelectListItem> location = (from x in db.Tbl_Konumlar.Where(x => x.DURUM == true)
                                             select new SelectListItem
                                             {
                                                 Text = x.KONUM,
                                                 Value = x.ID.ToString()
                                             }).ToList();
            ViewBag.Location = location;

            return View(value);
        }

        [HttpPost]
        public ActionResult UrunGuncelle(Tbl_Urunler p, HttpPostedFileBase RESIM)
        {
            var value = db.Tbl_Urunler.Find(p.ID);

            try
            {
                if (RESIM.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(Request.Files[0].FileName);
                    string extension = Path.GetExtension(Request.Files[0].FileName);
                    string date = Convert.ToString(DateTime.Now.ToLongDateString());
                    string time = Convert.ToString(DateTime.Now.ToLongTimeString()).Replace(":", "-");
                    string dateTime = date + "-" + time + "-";
                    string path = "~/Images/Products/" + dateTime + fileName;
                    string filePath = "/Images/Products/" + dateTime + fileName;
                    Request.Files[0].SaveAs(Server.MapPath(path));
                    value.RESIM = filePath;
                }
            }
            catch (Exception)
            {
                value.RESIM = value.RESIM;
            }

            var category = db.Tbl_Kategoriler.Where(x => x.ID == p.Tbl_Kategoriler.ID).FirstOrDefault();
            var location = db.Tbl_Konumlar.Where(x => x.ID == p.Tbl_Konumlar.ID).FirstOrDefault();

            value.Tbl_Kategoriler = category;
            value.Tbl_Konumlar = location;
            value.URUNADI = p.URUNADI;
            value.BARKODNO = p.BARKODNO;
            value.MARKA = p.MARKA;
            value.MODEL = p.MODEL;
            value.SERINO = p.SERINO;
            value.ACIKLAMA = p.ACIKLAMA;
            value.ARIZALIADET = p.ARIZALIADET;
            value.ADET = p.ADET;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunDetay(int id)
        {
            var value = db.Tbl_Urunler.Find(id);
            return View("UrunDetay", value);
        }

        public ActionResult UrunSil(Tbl_Urunler p)
        {
            var value = db.Tbl_Urunler.Find(p.ID);

            value.DURUM = false;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public PartialViewResult ZimmetSahipleri(int id)
        {
            var value = db.Tbl_Zimmetler.Where(x => x.ZIMMET == id && x.DURUM == true && x.Tbl_Personel.DURUM == true).ToList();
            return PartialView(value);
        }

        public ActionResult Yazdir()
        {
            var value = db.Tbl_Urunler.Where(x => x.DURUM == true && x.Tbl_Kategoriler.DURUM == true && x.Tbl_Konumlar.DURUM == true).ToList();
            return View(value);
        }


    }
}