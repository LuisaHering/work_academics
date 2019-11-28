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

        [HttpGet]
        public async Task<ActionResult> Home(int id) {
            var laboratory = new LaboratoryViewModel();
            laboratory.Id = 1;
            laboratory.Descricao = "Descricao do laboratorio";

            var membro1 = new MembrosOutputModel();
            membro1.Id = "123";
            membro1.Nome = "Membro 1";
            membro1.Email = "membro@membro.com";

            var membro2 = new MembrosOutputModel();
            membro2.Id = "321";
            membro2.Nome = "Membro 2";
            membro2.Email = "membro2@membro2.com";

            var projeto = new ProjetoOutputModel();
            projeto.Id = "123456";
            projeto.Titulo = "Titulo do projeto";
            projeto.Descricao = "Descricao do projeto";
            projeto.DataCriacao = DateTime.Now;
            projeto.laboratory = laboratory;
            projeto.Membros.Add(membro1);
            projeto.Membros.Add(membro2);

            projeto.laboratory = laboratory;

            var post = new PostViewModel();
            post.Id = "abc123";
            post.Mensagem = "mensagem 1";
            post.DataDePublicacao = DateTime.Now;
            post.Autor = "auto1";

            var post2 = new PostViewModel();
            post2.Id = "abc123";
            post2.Mensagem = "mensagem 1";
            post2.DataDePublicacao = DateTime.Now;
            post2.Autor = "auto2";

            projeto.Posts.Add(post);
            projeto.Posts.Add(post2);

            using(var client = new HttpClient()) {
                client.BaseAddress = new Uri(base_url);
                var response = await client.GetAsync($"api/project/busca?id={id}");

                if(response.IsSuccessStatusCode) {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    //DESCOMENTAR QUANDO ESTIVER CADASTRANDO OS POSTS
                    // projeto = JsonConvert.DeserializeObject<ProjetoOutputModel>(responseContent);
                    return View(projeto);
                }
            }

            return View();
        }
    }
}