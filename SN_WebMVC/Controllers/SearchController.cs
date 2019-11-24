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

            var email_logado = (Session["user_name"]).ToString();

            var id_seguido = (Session["profile_visita"]).ToString();

            var userProfile = new ProfileViewModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(base_url);
                var response = await client.GetAsync($"api/user/FindUser?email={email_logado}");

                var respondeContent = await response.Content.ReadAsStringAsync();
                userProfile = JsonConvert.DeserializeObject<ProfileViewModel>(respondeContent);
            }

            var data = new Dictionary<string, string>
            {
                {"IdSeguidor", userProfile.Id},
                {"IdSeguido", id_seguido}
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(base_url);

                using (var requestContent = new FormUrlEncodedContent(data))
                {
                    var response = await client.PostAsync("api/Follow", requestContent);

                    if (response.IsSuccessStatusCode)
                    {
                        Session.Remove("profile_visita");
                        //TODO: Mudar home para conexões
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            return View("Error");
        }
    }
}
