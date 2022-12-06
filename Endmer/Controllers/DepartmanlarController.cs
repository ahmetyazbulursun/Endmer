using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Endmer.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace Endmer.Controllers
{

    [Authorize(Roles = "A")]

    public class DepartmanlarController : Controller
    {

        EndmerEntities db = new EndmerEntities();

        public ActionResult Index(int page = 1, string ara = "")
        {
            var value = from x in db.Tbl_Departmanlar.Where(x => x.DURUM == true) select x;

            if(!string.IsNullOrEmpty(ara))
            {
                value = db.Tbl_Departmanlar.Where(x => x.DEPARTMAN.ToLower().Contains(ara) && x.DURUM == true);
            }

            return View(value.ToList().ToPagedList(page, 50));
        }

        [HttpGet]
        public ActionResult DepartmanEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DepartmanEkle(Tbl_Departmanlar p)
        {
            p.DURUM = true;

            db.Tbl_Departmanlar.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriSil(int id)
        {
            var value = db.Tbl_Departmanlar.Find(id);

            value.DURUM = false;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DepartmanGuncelle(int id)
        {
            var value = db.Tbl_Departmanlar.Find(id);
            return View("DepartmanGuncelle", value);
        }

        [HttpPost]
        public ActionResult DepartmanGuncelle(Tbl_Departmanlar p)
        {
            var value = db.Tbl_Departmanlar.Find(p.ID);

            value.DEPARTMAN = p.DEPARTMAN;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Personeller(int id, int page = 1)
        {
            var value = db.Tbl_Personel.Where(x => x.DEPARTMAN == id && x.DURUM == true).ToList().ToPagedList(page, 50);
            return View(value);
        }
        

    }
}