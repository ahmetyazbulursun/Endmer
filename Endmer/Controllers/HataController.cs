using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Endmer.Models.Entity;

namespace Endmer.Controllers
{
    public class HataController : Controller
    {

        EndmerEntities db = new EndmerEntities();

        public ActionResult Hata404()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }

        public ActionResult AnaSayfayaDon()
        {
            string userID = Convert.ToString(Session["ID"]);
            
            if (userID == "")
            {
                Session.Abandon();
                return RedirectToAction("Logout", "Login");
            }

            return RedirectToAction("Index", "AnaSayfa");
        }


    }
}