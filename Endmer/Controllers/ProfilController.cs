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
            int userID = Convert.ToInt32(Session["ID"]);

            var value = db.Tbl_Personel.Where(x => x.ID == id);

            int session = Convert.ToInt32(Session["ID"]);
            if (session != userID)
            {
                Session.Abandon();
                return RedirectToAction("Index", "Login");
            }

            return View("Index", value);

        }


    }
}