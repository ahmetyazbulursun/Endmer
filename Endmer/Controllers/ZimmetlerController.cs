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


    }
}