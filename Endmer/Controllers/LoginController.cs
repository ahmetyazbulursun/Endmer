using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Endmer.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace Endmer.Controllers
{

    [AllowAnonymous]

    public class LoginController : Controller
    {

        EndmerEntities db = new EndmerEntities();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Tbl_Personel p)
        {
            var value = db.Tbl_Personel.FirstOrDefault(x => x.KULLANICIADI == p.KULLANICIADI && x.PAROLA == p.PAROLA && x.DURUM == true);

            if (value != null && value.YETKI == "A")
            {
                FormsAuthentication.SetAuthCookie(value.KULLANICIADI, false);
                Session["ID"] = value.ID.ToString();
                Session["AD"] = value.AD.ToString();
                Session["SOYAD"] = value.SOYAD.ToString();
                Session["KULLANICIADI"] = value.KULLANICIADI.ToString();
                Session["PAROLA"] = value.PAROLA.ToString();
                Session["YETKI"] = value.YETKI.ToString();
                return RedirectToAction("Index", "AnaSayfa");
            }
            if (value != null && value.YETKI == "P")
            {
                FormsAuthentication.SetAuthCookie(value.KULLANICIADI, false);
                Session["ID"] = value.ID.ToString();
                Session["AD"] = value.AD.ToString();
                Session["SOYAD"] = value.SOYAD.ToString();
                Session["KULLANICIADI"] = value.KULLANICIADI.ToString();
                Session["PAROLA"] = value.PAROLA.ToString();
                Session["YETKI"] = value.YETKI.ToString();
                return RedirectToAction("Index", "Zimmetlerim");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

    }
}