using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Endmer.Models.Entity;

namespace Endmer.Controllers
{

    [Authorize(Roles = "A")]

    public class DokumantasyonController : Controller
    {

        EndmerEntities db = new EndmerEntities();

        public ActionResult Index()
        {
            if (Session["ID"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Logout", "Login");
            }

            var value = db.Tbl_Dokuman.Where(x => x.DURUM == true).ToList();
            return View(value);
        }

        public ActionResult DokumanDetay(int id)
        {
            var value = db.Tbl_Dokuman.Where(x => x.ID == id).ToList();
            return View(value);
        }

        [HttpGet]
        public ActionResult Yazi()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Yazi(Tbl_Dokuman p)
        {
            p.KONU = 1;
            p.DURUM = true;
            db.Tbl_Dokuman.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}