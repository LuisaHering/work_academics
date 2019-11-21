using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SN_WebMVC.Models;

namespace SN_WebMVC.Controllers {
    public class ProjectController : Controller {

        private static string base_url = "http://localhost:56435";

        public async Task<ActionResult> Index() {

            var access_email = Session["user_name"].ToString();
            var email = (Session["user_name"]).ToString();
            var projetos = new List<ProjectViewModel>();

            using(var client = new HttpClient()) {
                client.BaseAddress = new Uri(base_url);
                var response = await client.GetAsync($"api/project/busca?email={email}");

                if(response.IsSuccessStatusCode) {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    projetos = JsonConvert.DeserializeObject<List<ProjectViewModel>>(responseContent);
                }
            }
            IEnumerable<ProjectViewModel> lista = projetos;
            return View(lista);
        }

        public ActionResult Create() {

            var labs = new List<LaboratoryViewModel> {
                new LaboratoryViewModel { Id = 1, Descricao = "Codigods avancados C#" },
                new LaboratoryViewModel { Id = 2, Descricao = "Codigods avancados python" },
            };

            ViewBag.IdLaboratory = new SelectList(
                labs,
                "Id",
                "Descricao"
            );

            return View();
        }

        [HttpPost]
        public ActionResult Create(ProjectViewModel projeto) {


            return RedirectToAction("Index");
        }
    }
}