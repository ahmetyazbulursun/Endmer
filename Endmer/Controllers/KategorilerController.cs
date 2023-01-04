using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.Mvc;
using Endmer.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace Endmer.Controllers
{

    [Authorize(Roles = "A")]

    public class KategorilerController : Controller
    {

        EndmerEntities db = new EndmerEntities();

        public ActionResult Index(int page = 1)
        {
            if (Session["ID"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Logout", "Login");
            }

            var value = db.Tbl_Kategoriler.Where(x => x.DURUM == true).ToList().ToPagedList(page, 50);
            return View(value);
        }

        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KategoriEkle(Tbl_Kategoriler p)
        {
            if(!ModelState.IsValid)
            {
                return View("KategoriEkle");
            }

            p.DURUM = true;

            db.Tbl_Kategoriler.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult KategoriGuncelle(int id)
        {
            var value = db.Tbl_Kategoriler.Find(id);
            return View("KategoriGuncelle", value);
        }

        [HttpPost]
        public ActionResult KategoriGuncelle(Tbl_Kategoriler p)
        {
            if(!ModelState.IsValid)
            {
                return View("KategoriGuncelle");
            }

            var value = db.Tbl_Kategoriler.Find(p.ID);

            value.KATEGORIADI = p.KATEGORIADI;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriSil(Tbl_Kategoriler p)
        {
            var value = db.Tbl_Kategoriler.Find(p.ID);

            value.DURUM = false;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Urunler(int id, int page = 1)
        {
            var value = db.Tbl_Zimmetler.Where(x => x.Tbl_Urunler.KATEGORI == id && x.DURUM == true).ToList();
            return View(value);
        }

        public ActionResult Yazdir()
        {
            var value = db.Tbl_Kategoriler.Where(x => x.DURUM == true).ToList();
            return View(value);
        }

    }
}