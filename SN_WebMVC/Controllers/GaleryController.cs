using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SN_WebMVC.Controllers
{
    public class GaleryController : Controller
    {
        // GET: Galery
        public ActionResult Index()
        {
            return View();
        }
    }
}