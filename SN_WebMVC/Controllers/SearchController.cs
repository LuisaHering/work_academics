using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SN_WebMVC.Models;

namespace SN_WebMVC.Controllers {
    public class SearchController : Controller {

        [HttpPost]
        public ActionResult Search(string Pesquisar) {
            List<ProfileViewModel> profiles = new List<ProfileViewModel>();

            ProfileViewModel p1 = new ProfileViewModel() {
                Nome = "Carlos Henrique",
                Curso = "Engenharia de software"
            };

            ProfileViewModel p2 = new ProfileViewModel() {
                Nome = "Rafael foda-se",
                Curso = "Engenharia de software"
            };

            ProfileViewModel p3 = new ProfileViewModel() {
                Nome = "Gabriel foda-se",
                Curso = "Engenharia de software"
            };

            profiles.Add(p1);
            profiles.Add(p2);
            profiles.Add(p3);

            return View(profiles);
        }
    }
}
