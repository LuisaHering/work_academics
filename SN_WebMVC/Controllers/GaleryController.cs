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
    public class GaleryController : Controller {

        private static string base_url = "http://localhost:56435";

        public async Task<ActionResult> Index() {

            var fotos = new List<GaleryViewModel>();

            using(var client = new HttpClient()) {
                client.BaseAddress = new Uri(base_url);
                var response = await client.GetAsync($"api/picture/user?id_user={"9b587d78-268a-46cb-a25c-873df8e6f0d7"}");

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