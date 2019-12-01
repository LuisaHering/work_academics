using SN_WebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SN_WebMVC.App_Start;

namespace SN_WebMVC.Controllers {
    public class LaboratoryController : Controller {

        public async Task<ActionResult> Index() {
            var laboratories = new List<LaboratoryViewModel>();

            var access_token = (Session["access_token"]);
            var email = (Session["user_name"]).ToString();

            using(var client = new HttpClient()) {
                client.BaseAddress = new Uri(BaseUrl.URL);
                var response = await client.GetAsync($"api/Laboratory/busca?email={email}");

                if(response.IsSuccessStatusCode) {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    laboratories = JsonConvert.DeserializeObject<List<LaboratoryViewModel>>(responseContent);
                }
            }
            IEnumerable<LaboratoryViewModel> lista = laboratories;
            return View(lista);
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(FormCollection collection) {
            var Descricao = collection["Descricao"];
            var access_token = (Session["access_token"]);
            var EmailUsuario = (Session["user_name"]).ToString();

            var data = new Dictionary<string, string> {
                { "Descricao", Descricao },
                { "EmailUsuario", EmailUsuario }
            };

            using(var cliente = new HttpClient()) {
                cliente.BaseAddress = new Uri(BaseUrl.URL);
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                using(var requestContent = new FormUrlEncodedContent(data)) {
                    var response = await cliente.PostAsync("api/Laboratory/create", requestContent);

                    if(response.IsSuccessStatusCode) {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }

        public ActionResult Search() {
            ICollection<LaboratoryViewModel> laboratorios = new List<LaboratoryViewModel>();
            return View(laboratorios);
        }

        [HttpPost]
        public async Task<ActionResult> Search(string description) {
            var laboratorios = new List<LaboratoryViewModel>();

            using(var client = new HttpClient()) {
                client.BaseAddress = new Uri(BaseUrl.URL);
                var response = await client.GetAsync($"api/Laboratory/search?description={ description }");

                if(response.IsSuccessStatusCode) {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    laboratorios = JsonConvert.DeserializeObject<List<LaboratoryViewModel>>(responseContent);
                }
            }

            return View(laboratorios);
        }

        [HttpGet]
        public async Task<ActionResult> Home(int id) {
            var access_token = (Session["access_token"]);

            TempData["IdLaboratorio"] = id;

            var laboratory = new FullLaboratory();

            using(var client = new HttpClient()) {
                client.BaseAddress = new Uri(BaseUrl.URL);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                var response = await client.GetAsync($"api/Laboratory/home?id={ id }");

                if(response.IsSuccessStatusCode) {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    laboratory = JsonConvert.DeserializeObject<FullLaboratory>(responseContent);
                }
            }
            return View(laboratory);
        }

        [HttpPost]
        public async Task<ActionResult> EntrarNoLaboratorio() {
            var access_token = (Session["access_token"]);

            var EmailUsuario = (Session["user_name"]).ToString();
            var labId = TempData["IdLaboratorio"];

            var data = new Dictionary<string, string>()
            {
                {"IdLaboratorio", labId.ToString() },
                {"IdUsuario", EmailUsuario },
            };

            using(var client = new HttpClient()) {
                client.BaseAddress = new Uri(BaseUrl.URL);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                using(var requestContent = new FormUrlEncodedContent(data)) {
                    var response = await client.PostAsync("api/Laboratory/Entrar", requestContent);
                    if(response.IsSuccessStatusCode) {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View("Error");

        }

        [HttpPost]
        public async Task<ActionResult> SairDoLaboratorio() {
            var access_token = (Session["access_token"]);
            var EmailUsuario = (Session["user_name"]).ToString();
            var labId = TempData["IdLaboratorio"];

            var data = new Dictionary<string, string>()
            {
                {"IdLaboratorio", labId.ToString() },
                {"IdUsuario", EmailUsuario  },
            };

            using(var client = new HttpClient()) {
                client.BaseAddress = new Uri(BaseUrl.URL);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                using(var requestContent = new FormUrlEncodedContent(data)) {
                    var response = await client.PutAsync("api/Laboratory/Sair", requestContent);

                    if(response.IsSuccessStatusCode) {
                        return RedirectToAction("Index");
                    }
                }
                return View("Error");
            }
        }
    }
}
