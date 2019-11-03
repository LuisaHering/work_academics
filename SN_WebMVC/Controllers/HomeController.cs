using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SN_WebMVC.Controllers {
    public class HomeController : Controller {

        // GET: Home
        public ActionResult Index() {
            return View();
        }

        //get
        public ActionResult Perfil()
        {
            return View();
        }

        // GET: Home/Edit/
        public ActionResult Edit() {

            var access_token = Session["user_name"];

            return View();
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection) {
            try {
                
                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }
    }
}
