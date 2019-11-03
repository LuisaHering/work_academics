using Newtonsoft.Json;
using SN_WebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SN_WebMVC.Controllers {
    public class HomeController : Controller {

        private static string base_url = "http://localhost:56435";

        // GET: Home
        public ActionResult Index() {
            return View();
        }

        //get
        public ActionResult Perfil()
        {
            return View();
        }

        // GET: Home/Edit/
        public async Task<ActionResult> Edit() {
            var access_email = Session["user_name"];
            var access_token = Session["access_token"];

            ProfileViewModel profileView = new ProfileViewModel();

            using(var cliente = new HttpClient()) {
                cliente.BaseAddress = new Uri(base_url);

                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                var response = await cliente.GetAsync($"/api/account/findUser?email={access_email}");

                if(response.IsSuccessStatusCode) {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    profileView = JsonConvert.DeserializeObject<ProfileViewModel>(responseContent);

                    return View(profileView);
                }
            }
            return View();
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection) {

            // Criar obj do tipo esperado pelo endpoint
            // adicionar o token no headers
            // chamar requisicao
            // verificar status da requisicao
            // retornar para sucesso em caso de sucesso
            // retornar para erro em caso de erro

            try {                
                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }
    }
}
