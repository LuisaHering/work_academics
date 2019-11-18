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
            List<ProfileViewModel> profiles = new List<ProfileViewModel>();

            using(var client = new HttpClient()) {
                client.BaseAddress = new Uri(base_url);
                var response = await client.GetAsync($"api/user/search?name={pesquisar}");

                if(response.IsSuccessStatusCode) {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    profiles = JsonConvert.DeserializeObject<List<ProfileViewModel>>(responseContent);
                }
            }

            return View(profiles);
        }

        public async Task<ActionResult> Perfil(string id) {

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
    }
}
