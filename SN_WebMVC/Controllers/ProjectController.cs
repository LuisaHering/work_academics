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
        public async Task<ActionResult> Create(ProjectViewModel projeto) {
            var access_token = (Session["access_token"]);

            var data = new Dictionary<string, string> {
                { "Titulo", projeto.Titulo },
                { "Descricao", projeto.Descricao},
                { "IdLaboratory", projeto.IdLaboratory.ToString() }
            };

            using(var cliente = new HttpClient()) {
                cliente.BaseAddress = new Uri(base_url);

                using(var requestContent = new FormUrlEncodedContent(data)) {
                    var response = await cliente.PostAsync("api/Project/create", requestContent);

                    if(response.IsSuccessStatusCode) {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View("Error");
        }

        public async Task<ActionResult> Home(int id) {

            // buscar usuarios do projeto
            var usuarios = new List<string>();
            usuarios.Add("carlos@gmail.com");
            usuarios.Add("gabriel@gmail.com");
            usuarios.Add("rafael@gmail.com");
            usuarios.Add("lu@lu.com.br");


            var post = new PostViewModel() {
                Id = "1",
                Autor = "gabriel",
                DataDePublicacao = DateTime.Now,
                Mensagem = "conteudo conteudo conteudo conteudo conteudo conteudo conteudo conteudo conteudo conteudo conteudo conteudo conteudo conteudo conteudo conteudo conteudo conteudo conteudo conteudo conteudo conteudo conteudo conteudo conteudo conteudo conteudo conteudo conteudo conteudo conteudo conteudo conteudo conteudo ",
                UrlDocumento = "https://i.pinimg.com/originals/64/ed/5c/64ed5cee404ecd2f620426ba3788ab5f.jpg"
            };

            // buscar documentos do projeto
            var posts = new List<PostViewModel>();
            posts.Add(post);
            posts.Add(post);

            // buscar projeto
            FullProjectViewModel projeto = new FullProjectViewModel() {
                Posts = posts,
                Membros = usuarios,
                DataCriacao = DateTime.Now,
                Descricao = "descricao mockada",
                Id = "1",
                Titulo = "titulo mockado",
                IdLaboratory = 1
            };

            return View(projeto);
        }
    }
}