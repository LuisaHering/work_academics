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

namespace SN_WebMVC.Controllers {
    public class SearchController : Controller {

        private static string base_url = "http://localhost:56435";

        [HttpPost]
        public async Task<ActionResult> Search(string pesquisar) {
            var profiles = new List<ProfileViewModel>();

            var usuario_logado = (Session["user_name"]).ToString();

            using(var client = new HttpClient()) {
                client.BaseAddress = new Uri(base_url);
                var response = await client.GetAsync($"api/user/search?name={pesquisar}");

                if(response.IsSuccessStatusCode) {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    profiles = JsonConvert.DeserializeObject<List<ProfileViewModel>>(responseContent);
                }
            }

            var lista_tratada = new ProfileViewModel().RemoverUsuarioLogado(usuario_logado, profiles);

            return View(lista_tratada);
        }

        public async Task<ActionResult> Perfil(string id) {

            Session["profile_visita"] = id;

            ProfileViewModel profile = new ProfileViewModel();

            using(var client = new HttpClient()) {
                client.BaseAddress = new Uri(base_url);
                var response = await client.GetAsync($"api/user/FindUser?user_id={id}");

                if(response.IsSuccessStatusCode) {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    profile = JsonConvert.DeserializeObject<ProfileViewModel>(responseContent);
                }
            }

            return View(profile);
        }

        [HttpPost]
        public async Task<ActionResult> Seguir() {

            // buscar id do usuario logado


            // esse é o id do usuario que eu quero seguir
            var id_seguido = (Session["profile_visita"]).ToString();

            // modelo do json que vc precisa criar { "IdSeguidor": "id usuario logado", "IdSeguido": "id do seguido" }


            // realizar requisicao para endpoint api/Follow

            // retornar para home/index

            // ps: crie dois ou mais usuarios para testar


            return RedirectToAction("Index", "Home");
        }
    }
}
