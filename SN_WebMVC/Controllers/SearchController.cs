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
    public class SearchController : Controller {

        [HttpPost]
        public async Task<ActionResult> Search(string pesquisar) {
            var profiles = new List<ProfileViewModel>();

            var usuario_logado = (Session["user_name"]).ToString();

            using (var client = new HttpClient()) {
                client.BaseAddress = new Uri(BaseUrl.URL);
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
            var access_token = Session["access_token"];
            var access_email = Session["user_name"];
            Session["profile_visita"] = id;
            Session["ehAmigo"] = false;

            List<ProfileViewModel> conexoes = new List<ProfileViewModel>();

            var userProfile = new ProfileViewModel();

            using(var client = new HttpClient()) {
                client.BaseAddress = new Uri(BaseUrl.URL);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");
                var response = await client.GetAsync($"api/user/FindUser?email={access_email}");
                var respondeContent = await response.Content.ReadAsStringAsync();
                userProfile = JsonConvert.DeserializeObject<ProfileViewModel>(respondeContent);
            }

            using(var client = new HttpClient()) {
                client.BaseAddress = new Uri(BaseUrl.URL);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");
                var response = await client.GetAsync($"api/following/Conexoes?idUsuario={userProfile.Id.ToString()}");
                if(response.IsSuccessStatusCode) {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    conexoes = JsonConvert.DeserializeObject<List<ProfileViewModel>>(responseContent);
                }
            }

            ProfileViewModel profile = new ProfileViewModel();

            using(var client = new HttpClient()) {
                client.BaseAddress = new Uri(BaseUrl.URL);
                var response = await client.GetAsync($"api/user/FindUser?user_id={id}");

                if(response.IsSuccessStatusCode) {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    profile = JsonConvert.DeserializeObject<ProfileViewModel>(responseContent);
                }
            }

            foreach(ProfileViewModel p in conexoes) {
                if(p.Id.Equals(profile.Id)) {
                    Session["ehAmigo"] = true;
                }
            }

            return View(profile);
        }

        [HttpPost]
        public async Task<ActionResult> Seguir() {

            var email_logado = (Session["user_name"]).ToString();

            var id_seguido = (Session["profile_visita"]).ToString();

            var userProfile = new ProfileViewModel();

            using(var client = new HttpClient()) {
                client.BaseAddress = new Uri(BaseUrl.URL);
                var response = await client.GetAsync($"api/user/FindUser?email={email_logado}");

                var respondeContent = await response.Content.ReadAsStringAsync();
                userProfile = JsonConvert.DeserializeObject<ProfileViewModel>(respondeContent);
            }

            var data = new Dictionary<string, string>
            {
                {"IdSeguidor", userProfile.Id},
                {"IdSeguido", id_seguido}
            };

            using(var client = new HttpClient()) {
                client.BaseAddress = new Uri(BaseUrl.URL);

                using(var requestContent = new FormUrlEncodedContent(data)) {
                    var response = await client.PostAsync("api/following/Follow", requestContent);

                    if(response.IsSuccessStatusCode) {
                        Session.Remove("profile_visita");
                        return RedirectToAction("Conexoes");
                    }
                }
            }
            return View("Error");
        }

        [HttpPost]
        public async Task<ActionResult> Unfollow() {

            var email_logado = (Session["user_name"]).ToString();

            var id_seguido = (Session["profile_visita"]).ToString();

            var userProfile = new ProfileViewModel();

            using(var client = new HttpClient()) {
                client.BaseAddress = new Uri(BaseUrl.URL);
                var response = await client.GetAsync($"api/user/FindUser?email={email_logado}");

                var respondeContent = await response.Content.ReadAsStringAsync();
                userProfile = JsonConvert.DeserializeObject<ProfileViewModel>(respondeContent);
            }

            var data = new Dictionary<string, string>
            {
                {"IdSeguidor", userProfile.Id},
                {"IdSeguido", id_seguido}
            };

            using(var client = new HttpClient()) {
                client.BaseAddress = new Uri(BaseUrl.URL);

                using(var requestContent = new FormUrlEncodedContent(data)) {
                    var response = await client.PostAsync("api/following/Unfollow", requestContent);

                    if(response.IsSuccessStatusCode) {
                        Session.Remove("profile_visita");
                        return RedirectToAction("Conexoes");
                    }
                }
            }
            return View("Error");
        }

        [HttpGet]
        public async Task<ActionResult> Conexoes() {
            var access_token = Session["access_token"];
            var access_email = Session["user_name"];

            List<ProfileViewModel> conexoes = new List<ProfileViewModel>();

            var userProfile = new ProfileViewModel();

            using(var client = new HttpClient()) {
                client.BaseAddress = new Uri(BaseUrl.URL);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                var response = await client.GetAsync($"api/user/FindUser?email={access_email}");

                var respondeContent = await response.Content.ReadAsStringAsync();
                userProfile = JsonConvert.DeserializeObject<ProfileViewModel>(respondeContent);
            }

            using(var client = new HttpClient()) {
                client.BaseAddress = new Uri(BaseUrl.URL);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                var response = await client.GetAsync($"api/following/Conexoes?idUsuario={userProfile.Id.ToString()}");

                if(response.IsSuccessStatusCode) {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    conexoes = JsonConvert.DeserializeObject<List<ProfileViewModel>>(responseContent);
                    return View(conexoes);
                }
            }

            return View("Error");
        }

    }
}
