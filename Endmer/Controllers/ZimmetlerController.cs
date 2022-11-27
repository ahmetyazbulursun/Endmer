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
    public class ZimmetlerController : Controller
    {

        EndmerEntities db = new EndmerEntities();

        public ActionResult Index(int page = 1)
        {
            var value = db.Tbl_Zimmetler.Where(x => x.DURUM == true).ToList().ToPagedList(page, 50);
            return View(value);
        }


        [HttpGet]
        public ActionResult ZimmetVer()
        {
            List<SelectListItem> personnel = (from x in db.Tbl_Personel.Where(x => x.DURUM == true)
                                              select new SelectListItem
                                              {
                                                  Text = x.AD + " " + x.SOYAD,
                                                  Value = x.ID.ToString()
                                              }).ToList();
            ViewBag.Personnel = personnel;

            List<SelectListItem> product = (from x in db.Tbl_Urunler.Where(x => x.DURUM == true)
                                            select new SelectListItem
                                            {
                                                Text = x.URUNADI,
                                                Value = x.ID.ToString()
                                            }).ToList();
            ViewBag.Product = product;

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
        public ActionResult ZimmetVer(Tbl_Zimmetler p)
        {
            var personnel = db.Tbl_Personel.Where(x => x.ID == p.Tbl_Personel.ID).FirstOrDefault();
            var product = db.Tbl_Urunler.Where(x => x.ID == p.Tbl_Urunler.ID).FirstOrDefault();
            var location = db.Tbl_Konumlar.Where(x => x.ID == p.Tbl_Konumlar.ID).FirstOrDefault();

            p.Tbl_Personel = personnel;
            p.Tbl_Urunler = product;
            p.Tbl_Konumlar = location;
            p.DURUM = true;

            db.Tbl_Zimmetler.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}