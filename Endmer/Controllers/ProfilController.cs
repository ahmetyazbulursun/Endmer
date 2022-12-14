using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Endmer.Models.Entity;

namespace Endmer.Controllers
{
    public class ProfilController : Controller
    {

        EndmerEntities db = new EndmerEntities();

        public ActionResult Index(int id)
        {
            var value = db.Tbl_Personel.Where(x => x.ID == id).ToList();

            int userID = Convert.ToInt32(Session["ID"]);
            if (id != userID)
            {
                Session.Abandon();
                return RedirectToAction("Logout", "Login");
            }

            return View(value);
        }

        [HttpGet]
        public ActionResult ProfilGuncelle(int id)
        {
            var value = db.Tbl_Personel.Find(id);
            return View("ProfilGuncelle", value);
        }

        [HttpPost]
        public ActionResult ProfilGuncelle(Tbl_Personel p)
        {
            var value = db.Tbl_Personel.Find(p.ID);

            value.AD = p.AD;
            value.SOYAD = p.SOYAD;
            value.KULLANICIADI = "@" + p.KULLANICIADI;
            value.PAROLA = p.PAROLA;

            db.SaveChanges();
            return RedirectToAction("Logout", "Login");
        }


    }
}