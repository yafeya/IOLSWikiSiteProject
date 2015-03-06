using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IOLSWikiWebsite.Utilities;

namespace IOLSWikiWebsite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Insert()
        {
            return View();
        }

        public ActionResult Detail(int id)
        {
            return View(id);
        }

        public ActionResult Edit(int id)
        {
            return View(id);
        }
    }
}