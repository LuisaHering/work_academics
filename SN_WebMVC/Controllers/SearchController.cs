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
                Id = "1",
                Nome = "Carlos Henrique",
                Curso = "Engenharia de software",
                Foto = "data:image/jpeg;base64,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"
            };

            ProfileViewModel p2 = new ProfileViewModel() {
                Id = "2",
                Nome = "Rafael foda-se",
                Curso = "Engenharia de software",
                Foto = "https://cdn.britannica.com/s:575x450/75/162075-004-8B7E19E9.jpg"
            };

            ProfileViewModel p3 = new ProfileViewModel() {
                Id = "3",
                Nome = "Gabriel foda-se",
                Curso = "Engenharia de software",
                Foto = "https://www.petlove.com.br/images/breeds/193449/profile/original/poodle-p.jpg?1532539364"
            };

            profiles.Add(p1);
            profiles.Add(p2);
            profiles.Add(p3);

            return View(profiles);
        }
    }
}
