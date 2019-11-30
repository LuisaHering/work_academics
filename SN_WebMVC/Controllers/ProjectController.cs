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
using SN_WebMVC.Service.Projeto;
using SN_WebMVC.UploadExterno;

namespace SN_WebMVC.Controllers {
    public class ProjectController : Controller {

        private static string base_url = null;
        ProjetoService GetProjetoService = null;

        public ProjectController() {
            base_url = "http://localhost:56435";
            GetProjetoService = new ProjetoService();
        }

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
            var projeto = new ProjetoOutputModel();

            using(var client = new HttpClient()) {
                client.BaseAddress = new Uri(base_url);
                var response = await client.GetAsync($"api/project/busca?id={id}");

                if(response.IsSuccessStatusCode) {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    projeto = JsonConvert.DeserializeObject<ProjetoOutputModel>(responseContent);
                    var documentos = GetProjetoService.ListaDocumentos(projeto);

                    TempData["IdProjeto"] = projeto.Id;
                    TempData["IdLaboratorio"] = projeto.laboratory.Id;

                    ViewBag.Documentos = documentos;

                    return View(projeto);
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Home(FormCollection collection, HttpPostedFileBase Arquivo) {

            var id_projeto = Convert.ToInt32(TempData["IdProjeto"]);
            var id_laboratorio = Convert.ToInt32(TempData["IdLaboratorio"]);
            var email_usuario = (Session["user_name"]).ToString();


            string code_pdf = null;
            if(Arquivo != null) {
                code_pdf = Guid.NewGuid().ToString();
                new ServidorDeArquivo().UploadDeArquivo(Arquivo.InputStream, $"{code_pdf}.pdf");
            }

            var data = new Dictionary<string, string> {
                { "Mensagem", collection["Mensagem"] },
                { "EmailUsuario", email_usuario},
                { "UrlDocumento", code_pdf },
                { "IdProjeto", id_projeto.ToString() },
                { "IdLaboratorio", id_laboratorio.ToString() }
            };

            using(var cliente = new HttpClient()) {
                cliente.BaseAddress = new Uri(base_url);

                using(var requestContent = new FormUrlEncodedContent(data)) {
                    var response = await cliente.PostAsync("api/post", requestContent);

                    if(response.IsSuccessStatusCode) {
                        await Home(id_projeto);
                    }
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> EntrarNoProjeto()
        {
            var access_token = (Session["access_token"]);

            var EmailUsuario = (Session["user_name"]).ToString();
            var projetoId = TempData["IdProjeto"];

            var data = new Dictionary<string, string>()
            {
                {"IdProjeto", projetoId.ToString() },
                {"IdUsuario", EmailUsuario },
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(base_url);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                using (var requestContent = new FormUrlEncodedContent(data))
                {
                    var response = await client.PostAsync("api/project/Entrar", requestContent);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View("Error");

        }

        [HttpPost]
        public async Task<ActionResult> SairDoProjeto()
        {
            var access_token = (Session["access_token"]);

            var EmailUsuario = (Session["user_name"]).ToString();
            var projetoId = TempData["IdProjeto"];

            var data = new Dictionary<string, string>()
            {
                {"IdProjeto", projetoId.ToString() },
                {"IdUsuario", EmailUsuario },
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(base_url);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                using (var requestContent = new FormUrlEncodedContent(data))
                {
                    var response = await client.PutAsync("api/project/Sair", requestContent);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }

                }

                return View("Error");
            }
        }
    }
}