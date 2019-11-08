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
using Newtonsoft.Json;
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

            User oldUser = GetUsers.FindByEmail(bindingModel.EmailUsuario);

            Laboratory laboratory = new Laboratory() {
                User = oldUser,
                IdUser = oldUser.Id,
                Descricao = bindingModel.Descricao
            };

            oldUser.Adiciona(laboratory);

            var criouLab = GetUsers.Create(laboratory);
            var novoUsuario = GetUsers.UpdateEF2(oldUser);

            if(!criouLab || !novoUsuario) {
                return BadRequest("Erro interno");
            }
            return Ok(oldUser);
        }

        [HttpGet]
        [Route("busca")]
        public IHttpActionResult FindLabs(FindLaboratoryModels findLaboratory) {
            List<Laboratory> labs = GetLaboratory.FindAll(findLaboratory.Email);
            var aux = new LaboratoryReturnBindingModels();
            var result = aux.convert(labs);
            return Ok(result);
        }
    }
}