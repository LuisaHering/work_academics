﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SN_WebMVC.App_Start;
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
    public class AccountController : Controller {

        public ActionResult RecuperarSenha() {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RecuperarSenha(RecoverPassword model) {

            if(!model.Senha.Equals(model.ConfirmarSenha)) {
                ViewBag.Error = "Senhas não conferem";
                return View("Error");
            }

            var data = new Dictionary<string, string> {
                { "Email", model.Email },
                { "Password", model.Senha },
                { "ConfirmPassword", model.ConfirmarSenha }
            };

            using(var client = new HttpClient()) {
                client.BaseAddress = new Uri(BaseUrl.URL);

                using(var requestContent = new FormUrlEncodedContent(data)) {
                    var response = await client.PostAsync("/api/Account/change", requestContent);

                    if(response.IsSuccessStatusCode) {
                        return RedirectToAction("Login", "Account");
                    }

                    return View("Error");
                }
            }
        }

        public ActionResult Register() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model) {
            if(ModelState.IsValid) {
                var data = new Dictionary<string, string> {

                    { "grant_type", "password" },
                    { "Password", model.Password },
                    { "Email", model.Email},
                    { "ConfirmPassword", model.ConfirmPassword },
                    { "Name", model.Nome },
                    { "University", model.Universidade },
                };

                using(var client = new HttpClient()) {
                    client.BaseAddress = new Uri("http://localhost:56435");

                    using(var requestContent = new FormUrlEncodedContent(data)) {
                        var verificaEmail = await client.GetAsync($"/api/user/findUser?email={model.Email}");

                        if(verificaEmail.IsSuccessStatusCode) {
                            ViewBag.Error = "Este email já está sendo usado.";
                            return View("Error");
                        }

                        var response = await client.PostAsync("Api/Account/Register", requestContent);
                        if(response.IsSuccessStatusCode) {
                            return RedirectToAction("Login");
                        } else {
                            return View("Error");
                        }
                    }
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Logout() {
            var access_token = Session["access_token"];

            using(var cliente = new HttpClient()) {
                cliente.BaseAddress = new Uri(BaseUrl.URL);
                cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{access_token}");

                var response = await cliente.GetAsync("/api/Account/Logout");
                if(response.IsSuccessStatusCode) {
                    return RedirectToAction("Login", "Account");
                }
            }
            return RedirectToAction("Error", "Shared");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model) {

            if(ModelState.IsValid) {
                var data = new Dictionary<string, string> {
                    { "grant_type", "password" },
                    { "username", model.Username },
                    { "password", model.Password }
                };

                using(var client = new HttpClient()) {
                    client.BaseAddress = new Uri("http://localhost:56435");

                    using(var requestContent = new FormUrlEncodedContent(data)) {
                        var response = await client.PostAsync("/Token", requestContent);
                        var response_user_data = await client.GetAsync($"/api/user/findUser?email={model.Username}");

                        if(response_user_data.IsSuccessStatusCode) {
                            var responseUser = await response_user_data.Content.ReadAsStringAsync();
                            ProfileViewModel profileView = new ProfileViewModel();

                            profileView = JsonConvert.DeserializeObject<ProfileViewModel>(responseUser);
                            Session.Add("picture_profile", profileView.Foto);
                        }

                        if(response.IsSuccessStatusCode) {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            var tokenData = JObject.Parse(responseContent);

                            Session.Add("access_token", tokenData["access_token"]);
                            Session.Add("user_name", model.Username);
                            return RedirectToAction("Index", "Home");
                        }
                        //return View();
                    }
                }
            }
            return View();
        }

        public ActionResult Login() {
            return View();
        }
    }
}