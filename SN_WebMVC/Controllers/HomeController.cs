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

        //get home/perfil
        public async Task<ActionResult> Perfil()
        {
            var access_email = Session["user_name"];
            var access_token = Session["access_token"];

            ProfileViewModel profileView = new ProfileViewModel();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(base_url);

                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                var response = await cliente.GetAsync($"/api/account/findUser?email={access_email}");

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    profileView = JsonConvert.DeserializeObject<ProfileViewModel>(responseContent);

                    return View(profileView);
                }
            }

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
        public async Task<ActionResult> Edit(FormCollection collection) {

            var access_token = Session["access_token"];

            if (ModelState.IsValid)
            {
                var data = new Dictionary<string, string> {

                    { "grant_type", "password" },
                    { "Email", collection["Email"]},
                    { "Nome", collection["Nome"] },
                    { "Universidade", collection["Universidade"] },
                    { "Curso", collection["Curso"] },
                    { "Biografia", collection["Biografia"] },
                };

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri("http://localhost:56435");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                    using (var requestContent = new FormUrlEncodedContent(data))
                    {
                        var response = await client.PutAsync("Api/Account/update", requestContent);
                        
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Perfil");
                        }
                        else
                        {
                            return View("Error");
                        }
                    }
                }
            }

        }


    }
}
