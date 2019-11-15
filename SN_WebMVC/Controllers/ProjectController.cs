using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SN_WebMVC.Models;

namespace SN_WebMVC.Controllers {
    public class ProjectController : Controller {

        public async Task<ActionResult> Index() {
            List<ProjectViewModel> l = new List<ProjectViewModel>();
            ProjectViewModel p1 = new ProjectViewModel() {
                Id = 1,
                Descricao = "PROJETO 1",
                Titulo = "PROJETO 1",
                DataCriacao = DateTime.Now
            };

            l.Add(p1);
            IEnumerable<ProjectViewModel> lista = l;

            return View(lista);
        }

        public ActionResult Create() {

            var list = new List < LaboratoryViewModel > {
                new LaboratoryViewModel { Id = 1, Descricao = "Codigods avancados C#" },
                new LaboratoryViewModel { Id = 1, Descricao = "Codigods avancados python" },
            };

            ViewBag.Id = new SelectList(
                list,
                "Id",
                "Descricao"
            );

            return View();
        }

    }
}
