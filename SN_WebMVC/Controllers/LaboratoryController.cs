using SN_WebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SN_WebMVC.Controllers {
    public class LaboratoryController : Controller {

        private static string base_url = "http://localhost:56435";

        // GET: Laboratory
        public ActionResult Index() {
            List<LaboratoryViewModel> laboratories = new List<LaboratoryViewModel>();

            var l = new LaboratoryViewModel() {
                Id = 1,
                Descricao = "LAB_INF"
            };

            laboratories.Add(l);

            IEnumerable<LaboratoryViewModel> lista = laboratories;

            return View(lista);
        }
    }
}