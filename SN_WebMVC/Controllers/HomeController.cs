﻿using Newtonsoft.Json;
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

        public void UploadDeFoto(HttpPostedFileBase foto)
        {
            //ServidorDeArquivos servidorDeArquivos = new ServidorDeArquivos();

            //servidorDeArquivos.UploadDeArquivo(foto.InputStream, foto.FileName);

            //if (ModelState.IsValid)
            //{
            //    db.Pessoas.Add(pessoa);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //return View(pessoa);
        }

        public ActionResult Index() {
            return View();
        }

        public async Task<ActionResult> Perfil() {
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

            Session.Add("access_token", access_token);
            Session.Add("access_email", access_email);
            return View();
        }

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

            Session.Add("access_token", access_token);
            Session.Add("access_email", access_email);

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(FormCollection collection) {

            var access_token = Session["access_token"];
            var access_email = Session["user_name"];

            if(ModelState.IsValid) {
                var data = new Dictionary<string, string> {

                    { "grant_type", "password" },
                    { "Email", collection["Email"]},
                    { "Nome", collection["Nome"] },
                    { "Universidade", collection["Universidade"] },
                    { "Curso", collection["Curso"] },
                    { "Biografia", collection["Biografia"] },
                };

                using(var client = new HttpClient()) {

                    client.BaseAddress = new Uri("http://localhost:56435");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                    using(var requestContent = new FormUrlEncodedContent(data)) {
                        var response = await client.PutAsync("Api/Account/update", requestContent);

                        if(response.IsSuccessStatusCode) {
                            return RedirectToAction("Perfil");
                        } else {
                            return View("Error");
                        }
                    }
                }
            }

            Session.Add("access_token", access_token);
            Session.Add("access_email", access_email);

            return View();

        }

    }
}
