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
using SN_WebApi.Models.Laboratorio;
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
        public async Task<IHttpActionResult> CreateAsync(LaboratoryBindingModel bindingModel) {

            User oldUser = await GetUsers.FindByEmail(bindingModel.EmailUsuario);

            Laboratory laboratory = new Laboratory() {
                Descricao = bindingModel.Descricao,
            };

            laboratory.Adiciona(oldUser);

            oldUser.Adiciona(laboratory);

            var criouLab = await GetLaboratory.Create(laboratory);

            if(!oldUser.haveRole()) {
                Role coordenador = new Role() {
                    Id = 2,
                    Descricao = "COORDENADOR"
                };
                oldUser.Role = coordenador;
            }

            var atualizado = GetUsers.UpdateEF2(oldUser);

            if(!criouLab || !atualizado) {
                return BadRequest("Erro interno");
            }

            return Ok();
        }

        [HttpGet]
        [Route("busca")]
        public async Task<IHttpActionResult> FindLaboratory(string email) {
            List<Laboratory> labs = await GetLaboratory.FindByEmail(email);
            var aux = new LaboratoryReturnBindingModels();
            var result = aux.convert(labs);
            return Ok(result);
        }

        [HttpGet]
        [Route("home")]
        public async Task<IHttpActionResult> Home(int id) {
            Laboratory lab = await GetLaboratory.FindByIdAsync(id);
            var convertido = new FullLaboratoryBindingModel().Convert(lab);
            return Ok(convertido);
        }

        [HttpGet]
        [Route("search")]
        public IHttpActionResult FindLaboratories(string description) {
            List<Laboratory> labs = GetLaboratory.SearchLaboratoryBy(description);
            var aux = new LaboratoryReturnBindingModels();
            var result = aux.convert(labs);
            return Ok(result);
        }

        [HttpPost]
        [Route("Entrar")]
        public async Task<IHttpActionResult> EntrarNoLaboratorioAsync(EntrarNoLaboratorio request) {

            var laboratorio = (Laboratory)await GetLaboratory.FindByIdAsync(request.IdLaratorio);
            var usuario = (User)await GetUsers.FindById(request.IdUsuario);

           
            if(!usuario.estaNoLaboratorio(laboratorio, usuario.Id.ToString())) {
                laboratorio.Adiciona(usuario);
                var atualizou = await GetLaboratory.Update(laboratorio);
                if(atualizou)
                    return Ok();
            }

            return BadRequest("Erro ao processar a solicitacao");
        }
    }
}