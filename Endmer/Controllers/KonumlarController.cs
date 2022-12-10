using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Endmer.Models.Entity;
using Newtonsoft.Json.Linq;

namespace Endmer.Controllers
{

    [Authorize(Roles = "A")]

    public class KonumlarController : Controller
    {

        EndmerEntities db = new EndmerEntities();

        public ActionResult Index(int page = 1)
        {
            var value = db.Tbl_Konumlar.Where(x => x.DURUM == true).ToList().ToPagedList(page, 50);
            return View(value);
        }

        [HttpGet]
        public ActionResult KonumEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KonumEkle(Tbl_Konumlar p)
        {
            p.DURUM = true;

            db.Tbl_Konumlar.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriSil(Tbl_Konumlar p)
        {
            var value = db.Tbl_Konumlar.Find(p.ID);

            value.DURUM = false;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult KonumGuncelle(int id)
        {
            var value = db.Tbl_Konumlar.Find(id);
            return View("KonumGuncelle", value);
        }

        [HttpPost]
        public ActionResult KonumGuncelle(Tbl_Konumlar p)
        {
            var value = db.Tbl_Konumlar.Find(p.ID);

            value.KONUM = p.KONUM;

            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }

}