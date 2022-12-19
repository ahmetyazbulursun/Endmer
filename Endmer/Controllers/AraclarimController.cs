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
    public class AraclarimController : Controller
    {

        EndmerEntities db = new EndmerEntities();

        public ActionResult Index(int page = 1)
        {
            if (Session["ID"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Logout", "Login");
            }

            int userID = Convert.ToInt32(Session["ID"]);
            var value = db.Tbl_Araclar.Where(x => x.KULLANICI == userID && x.DURUM == true).ToList().ToPagedList(page, 50);
            return View(value);
        }

        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            var value = db.Tbl_Araclar.Find(id);
            return View(value);
        }

        [HttpPost]
        public ActionResult Guncelle(Tbl_Araclar p, Tbl_AracKayit k)
        {
            if(!ModelState.IsValid)
            {
                return View("Guncelle");
            }

            var value = db.Tbl_Araclar.Find(p.ID);
            var vehicle = db.Tbl_Araclar.Find(p.ID);

            value.KM = p.KM;

            k.ARAC = vehicle.ID;
            k.TESLIMEDEN = vehicle.KULLANICI;
            k.TESLIMALAN = vehicle.KULLANICI;
            k.KM = p.KM;
            k.LOKASYON = vehicle.LOKASYON;
            k.TARIH = DateTime.Now;
            k.DURUM = true;

            db.Tbl_AracKayit.Add(k);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}