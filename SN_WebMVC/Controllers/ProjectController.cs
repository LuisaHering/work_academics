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

        public async Task<ActionResult> Create() {

            var laboratories = new List<LaboratoryViewModel>();

            var email = (Session["user_name"]).ToString();

            using(var client = new HttpClient()) {
                client.BaseAddress = new Uri(base_url);
                var response = await client.GetAsync($"api/Laboratory/busca?email={email}");

                if(response.IsSuccessStatusCode) {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    laboratories = JsonConvert.DeserializeObject<List<LaboratoryViewModel>>(responseContent);
                }
            }

            ViewBag.IdLaboratory = new SelectList(
                laboratories,
                "Id",
                "Descricao"
            );

            return View();
        }

        [HttpPost]
        public ActionResult Create(ProjectViewModel projeto) {
            // colocar dados no dicionario
            // abrir httpclient
            // dar um post em /api/project/create
            

            return RedirectToAction("Index");
        }
    }
}