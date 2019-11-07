using SN_WebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace SN_WebMVC.Controllers {
    public class LaboratoryController : Controller {

        private static string base_url = "http://localhost:56435";

        // Deve buscar os laboratorios do usuario logado
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

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Create(FormCollection collection) {

            var laboratory_name = collection["Descricao"];
            var access_token = (Session["access_token"]);
            var user_name = (Session["user_name"]);

            var data = new Dictionary<string, string> {
                { "laboratory_name", laboratory_name },
                { "user_name", laboratory_name }
            };

            using(var cliente = new HttpClient()) {
                cliente.BaseAddress = new Uri(base_url);
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                using(var requestContent = new FormUrlEncodedContent(data)) {
                    var response = await cliente.PostAsync("api/Laboratory/create", requestContent);

                }
            }

            return RedirectToAction("Index");
        }
    }
}