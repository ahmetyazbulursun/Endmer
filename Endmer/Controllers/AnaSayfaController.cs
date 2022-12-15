using Endmer.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Endmer.Controllers
{

    [Authorize(Roles = "A")]

    public class AnaSayfaController : Controller
    {

        EndmerEntities db = new EndmerEntities();

        public ActionResult Index()
        {
            if (Session["ID"] == null)
            {
                Session.Abandon();
                return RedirectToAction("Logout", "Login");
            }

            return View();
        }

        public PartialViewResult Cards()
        {
            var totalPersonal = db.Tbl_Personel.Where(x => x.DURUM == true).Count().ToString();
            ViewBag.TotalPersonal = totalPersonal;

            var totalProduct = db.Tbl_Urunler.Where(x => x.DURUM == true).Count().ToString();
            ViewBag.TotalProduct = totalProduct;

            var totalDebit = db.Tbl_Zimmetler.Where(x => x.DURUM == true).Count().ToString();
            ViewBag.TotalDebit = totalDebit;

            var totalVehicles = db.Tbl_Araclar.Where(x => x.DURUM == true).Count().ToString();
            ViewBag.TotalVehicles = totalVehicles;

            return PartialView();
        }

        public PartialViewResult ProductByCategory()
        {
            var value = db.Tbl_Kategoriler.Where(x => x.DURUM == true).ToList();
            return PartialView(value);
        }

        public PartialViewResult PersonalByDepartman()
        {
            var value = db.Tbl_Departmanlar.Where(x => x.DURUM == true).ToList();
            return PartialView(value);
        }


    }
}