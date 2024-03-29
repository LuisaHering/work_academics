﻿using Newtonsoft.Json;
using SN_WebMVC.App_Start;
using SN_WebMVC.Models;
using SN_WebMVC.UploadExterno;
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

        public HomeController() {
            
        }

        public async Task<ActionResult> Index() {
            var access_email = Session["user_name"];
            ProfileViewModel profileView = new ProfileViewModel();

            using(var cliente = new HttpClient()) {
                cliente.BaseAddress = new Uri(BaseUrl.URL);

                var response = await cliente.GetAsync($"/api/user/findUser?email={access_email}");

                if(response.IsSuccessStatusCode) {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    profileView = JsonConvert.DeserializeObject<ProfileViewModel>(responseContent);
                    await ConsultarPost(profileView.Id);
                }
            }
            Session.Add("picture_profile", profileView.Foto);
            return View();
        }

        private async Task ConsultarPost(string profileid) {
            List<PostViewModel> posts = new List<PostViewModel>();

            using(var cliente = new HttpClient()) {
                cliente.BaseAddress = new Uri(BaseUrl.URL);

                var resposta = await cliente.GetAsync($"api/Post?iduser={profileid}");
                if(resposta.IsSuccessStatusCode) {
                    var conteudoDaRespost = await resposta.Content.ReadAsStringAsync();
                    posts = JsonConvert.DeserializeObject<List<PostViewModel>>(conteudoDaRespost);
                    ViewBag.Posts = posts;
                }
            }
        }

        public async Task<ActionResult> Perfil() {
            var access_email = Session["user_name"];
            var access_token = Session["access_token"];

            ProfileViewModel profileView = new ProfileViewModel();

            using(var cliente = new HttpClient()) {
                cliente.BaseAddress = new Uri(BaseUrl.URL);

                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                var response = await cliente.GetAsync($"/api/user/findUser?email={access_email}");

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
                cliente.BaseAddress = new Uri(BaseUrl.URL);

                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                var response = await cliente.GetAsync($"/api/user/findUser?email={access_email}");

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
        public async Task<ActionResult> Edit(FormCollection collection, HttpPostedFileBase foto) {

            var access_token = Session["access_token"];
            var access_email = Session["user_name"];

            string code_img = null;

            if(foto != null) {
                code_img = Guid.NewGuid().ToString();
                new ServidorDeArquivo().UploadDeArquivo(foto.InputStream, $"{code_img}.png");
            }

            if(ModelState.IsValid) {
                var data = new Dictionary<string, string> {

                    { "grant_type", "password" },
                    { "Email", collection["Email"]},
                    { "Nome", collection["Nome"] },
                    { "Universidade", collection["Universidade"] },
                    { "Curso", collection["Curso"] },
                    { "Biografia", collection["Biografia"] },
                    { "CodeIMG", code_img },
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
