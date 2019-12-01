using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SN_WebMVC.App_Start;
using SN_WebMVC.Models;

namespace SN_WebMVC.Controllers {
    public class GaleryController : Controller {

        public async Task<ActionResult> Index() {

            var fotos = new List<GaleryViewModel>();
            var user_email = (Session["user_name"]).ToString();
            var profileView = new ProfileViewModel();

            using(var client = new HttpClient()) {
                client.BaseAddress = new Uri(BaseUrl.URL);
                var user_response = await client.GetAsync($"/api/user/findUser?email={user_email}");
                var responseContent = await user_response.Content.ReadAsStringAsync();
                profileView = JsonConvert.DeserializeObject<ProfileViewModel>(responseContent);
            }

            using(var client = new HttpClient()) {
                client.BaseAddress = new Uri(BaseUrl.URL);
                var response = await client.GetAsync($"api/picture/user?id_user={profileView.Id}");

                if(response.IsSuccessStatusCode) {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    fotos = JsonConvert.DeserializeObject<List<GaleryViewModel>>(responseContent);
                }
            }
            IEnumerable<GaleryViewModel> lista = fotos;
            return View(lista);
        }
    }
}