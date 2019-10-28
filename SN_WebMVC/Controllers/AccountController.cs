using Newtonsoft.Json.Linq;
using SN_WebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SN_WebMVC.Controllers {
    public class AccountController : Controller {
        // get Account/RecuperarSenha
        public ActionResult RecuperarSenha() {
            return View();
        }

        // post
        public ActionResult TrocaSenha() {
            return View();
        }

        //Get: Account/Register
        public ActionResult Register() {
            return View();
        }

        //Post: Account/Register
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
                    { "Unersity", model.Universidade }
                };

                using(var client = new HttpClient()) {

                    client.BaseAddress = new Uri("http://localhost:56435");

                    using(var requestContent = new FormUrlEncodedContent(data)) {

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

        //POST: Account/Login
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

                        if(response.IsSuccessStatusCode) {
                            var responseContent = await response.Content.ReadAsStringAsync();

                            var tokenData = JObject.Parse(responseContent);

                            Session.Add("acess_Token", tokenData["acess_token"]);
                            Session.Add("user_name", model.Username);
                            //Session.Add("user_")

                            return RedirectToAction("Index", "Home");
                        }

                        return View("Error");
                    }
                }
            }
            return View();
        }

        // GET: Account
        public ActionResult Login() {

            return View();
        }


        //colar code john




    }
}