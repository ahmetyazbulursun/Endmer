using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Endmer.Models.Entity;

namespace Endmer.Controllers
{
    public class DokumantasyonController : Controller
    {

        EndmerEntities db = new EndmerEntities();

        public ActionResult Index()
        {
            return View();
        }


    }
}