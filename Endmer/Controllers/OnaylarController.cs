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
    public class OnaylarController : Controller
    {

        EndmerEntities db = new EndmerEntities();

        public ActionResult Index(int page = 1)
        {
            var value = db.Tbl_ZimmetAktar.Where(x => x.DURUM == true).ToList().ToPagedList(page, 50);
            return View(value);
        }

        public ActionResult Onayla(Tbl_ZimmetAktar p, Tbl_Zimmetler z)
        {
            var value = db.Tbl_ZimmetAktar.Find(p.ID);
            var debit = db.Tbl_Zimmetler.Find(z.ID);

            value.ONAYDURUM = true;
            value.DURUM = false;
            debit.DURUM = false;
            debit.ONAYMESAJ = "Onaylandı";

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Reddet(Tbl_ZimmetAktar p, Tbl_Zimmetler z)
        {
            var value = db.Tbl_ZimmetAktar.Find(p.ID);
            var debit = db.Tbl_Zimmetler.Find(z.ID);

            value.ONAYDURUM = false;
            value.DURUM = false;
            debit.ONAYMESAJ = "Reddedildi";

            db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}