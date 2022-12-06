using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Endmer.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace Endmer.Controllers
{

    [Authorize(Roles = "A")]

    public class UrunlerController : Controller
    {

        EndmerEntities db = new EndmerEntities();

        public ActionResult Index(int page = 1, string ara = "")
        {
            var value = from x in db.Tbl_Urunler.Where(x => x.DURUM == true) select x;

            if (!string.IsNullOrEmpty(ara))
            {
                value = value.Where(x => x.URUNADI.ToLower().Contains(ara));
            }

            return View(value.ToList().ToPagedList(page, 50));
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
        public ActionResult UrunEkle(Tbl_Urunler p)
        {
            var category = db.Tbl_Kategoriler.Where(x => x.ID == p.Tbl_Kategoriler.ID).FirstOrDefault();
            var location = db.Tbl_Konumlar.Where(x => x.ID == p.Tbl_Konumlar.ID).FirstOrDefault();

            p.Tbl_Kategoriler = category;
            p.Tbl_Konumlar = location;
            p.DURUM = true;
            p.RESIM = "/template/default-img.jpg";

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
                    string filePath = Path.Combine(Server.MapPath("~/Images/Products"), Path.GetFileName(RESIM.FileName));
                    RESIM.SaveAs(filePath);

                    string fileName = Path.GetFileName(Request.Files[0].FileName);
                    string extension = Path.GetExtension(Request.Files[0].FileName);
                    string path = "~/Images/Products/" + fileName;
                    Request.Files[0].SaveAs(Server.MapPath(path));
                    value.RESIM = "/Images/Products/" + fileName;
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
            value.URUNDURUM = p.URUNDURUM;

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

    }
}