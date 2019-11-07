using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Core.Models;
using Core.Services;
using Data.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using SN_WebApi.Models;
using SN_WebApi.Providers;
using SN_WebApi.Results;
using SN_WebApi.Service;

namespace SN_WebApi.Controllers {

    [RoutePrefix("api/Laboratory")]
    public class LaboratoryController : ApiController {

        private IUsers GetUsers = ServiceLocator.GetInstanceOf<UsersImpl>();
        private ILaboratory GetLaboratory = ServiceLocator.GetInstanceOf<LaboratoryImpl>();

        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create(LaboratoryBindingModel bindingModel) {

            User u = GetUsers.FindByEmail(bindingModel.EmailUsuario);

            Laboratory l = new Laboratory();
            l.User = u;
            l.IdUser = u.Id;
            l.Descricao = bindingModel.Descricao;
            

            u.Laboratories.Add(l);


            GetLaboratory.Create(l);


           

            return null;
        }
    }
}