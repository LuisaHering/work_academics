using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SN_WebMVC.Controllers
{
    public class ValueController : Controller
    {
        // GET: Value
        public async Task<ActionResult> Index()
        {
            string access_token = Session["access_token"]?.ToString();
            if(!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:56624");
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", $"{access_token}");

                    var response = await client.GetAsync("/api/Values");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        return RedirectToAction("Index", "Home");

                    }
                    return View("Error");
                }
            }

            return RedirectToAction("Login", "Account", null);

        }
    }
}