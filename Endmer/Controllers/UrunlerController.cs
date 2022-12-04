using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Endmer.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace Endmer.Controllers
{
    public class UrunlerController : Controller
    {

        EndmerEntities db = new EndmerEntities();

        public ActionResult Index(int page = 1)
        {
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

            return View();
        }

        [HttpPost]
        public ActionResult UrunEkle(Tbl_Urunler p)
        {
            //if(RESIM.ContentLength > 0)
            //{
            //    string filePath = Path.Combine(Server.MapPath("~/Images/Products"), Path.GetFileName(RESIM.FileName));
            //    RESIM.SaveAs(filePath);

            //    string fileName = Path.GetFileName(Request.Files[0].FileName);
            //    string extension = Path.GetExtension(Request.Files[0].FileName);
            //    string path = "~/Images/Products/" + fileName;
            //    Request.Files[0].SaveAs(Server.MapPath(path));
            //    p.RESIM = "/Images/Products/" + fileName;
            //}

            var category = db.Tbl_Kategoriler.Where(x => x.ID == p.Tbl_Kategoriler.ID).FirstOrDefault();

            p.Tbl_Kategoriler = category;
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

            return View(value);
        }

        [HttpPost]
        public ActionResult UrunGuncelle(Tbl_Urunler p, HttpPostedFileBase RESIM)
        {
            var value = db.Tbl_Urunler.Find(p.ID);

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

            var category = db.Tbl_Kategoriler.Where(x => x.ID == p.Tbl_Kategoriler.ID).FirstOrDefault();

            value.Tbl_Kategoriler = category;
            value.URUNADI = p.URUNADI;
            value.BARKODNO = p.BARKODNO;
            value.MARKA = p.MARKA;
            value.MODEL = p.MODEL;
            value.SERINO = p.SERINO;
            value.ACIKLAMA = p.ACIKLAMA;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunDetay(int id)
        {
            var value = db.Tbl_Urunler.Find(id);
            return View("UrunDetay", value);
        }

        public PartialViewResult ZimmetSahipleri(int id)
        {
            var value = db.Tbl_Zimmetler.Where(x => x.ZIMMET == id && x.DURUM == true).ToList();
            return PartialView(value);
        }

    }
}