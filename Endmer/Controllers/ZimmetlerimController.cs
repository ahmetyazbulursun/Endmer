using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Endmer.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace Endmer.Controllers
{
    public class ZimmetlerimController : Controller
    {

        EndmerEntities db = new EndmerEntities();

        public ActionResult Index(int page = 1)
        {
            int userID = Convert.ToInt32(Session["ID"]);
            var value = db.Tbl_Zimmetler.Where(x => x.PERSONEL == userID).ToList().ToPagedList(page, 50);
            return View(value);
        }
    }
}