using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
        public async Task<ActionResult> Create(ProjectViewModel projeto)
        {
            if (ModelState.IsValid)
            {
                string criacaoData = $"{projeto.DataCriacao.Day}-{projeto.DataCriacao.Month}-{projeto.DataCriacao.Year}";

                var access_token = (Session["access_token"]);


                //FIXME: Não sei como ou onde colocar a IdLaboratory, também n entendo como fica salvo qual usuário que criou o projeto
                var data = new Dictionary<string, string>
                {


                    { "Titulo", projeto.Titulo },
                    { "Descrição", projeto.Descricao},
                    { "Data Criação", criacaoData },

                };

                using (var cliente = new HttpClient())
                {
                    cliente.BaseAddress = new Uri(base_url);
                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                    using (var requestContent = new FormUrlEncodedContent(data))
                    {
                        var response = await cliente.PostAsync("api/Project/create", requestContent);

                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }

                return View();
                // colocar dados no dicionario
                // abrir httpclient
                // dar um post em /api/project/create
            }
        }
        
    }
}