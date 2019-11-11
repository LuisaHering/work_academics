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
    public class ProjectViewModelsController : Controller {

        public async Task<ActionResult> Index() {
            List<ProjectViewModel> l = new List<ProjectViewModel>();
            ProjectViewModel p1 = new ProjectViewModel() {
                Id = 1,
                Descricao = "PROJETO 1",
                Finalidade = "PROJETO 1",
                Titulo = "PROJETO 1",
                DataCriacao = DateTime.Now
            };

            l.Add(p1);
            IEnumerable<ProjectViewModel> lista = l;
            return View(lista);
        }
    }
}
