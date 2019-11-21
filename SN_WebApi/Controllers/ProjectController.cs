﻿using System;
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
using SN_WebApi.Models.Projeto;
using SN_WebApi.Providers;
using SN_WebApi.Results;
using SN_WebApi.Service;

namespace SN_WebApi.Controllers {

    [RoutePrefix("api/project")]
    public class ProjectController : ApiController {

        private IUsers GetUsers = ServiceLocator.GetInstanceOf<UsersImpl>();
        private ILaboratory GetLaboratory = ServiceLocator.GetInstanceOf<LaboratoryImpl>();
        private IProject GetProject = ServiceLocator.GetInstanceOf<ProjectImpl>();

        [HttpGet]
        [Route("busca")]
        public async Task<IHttpActionResult> FindLaboratoryBy(string email) {
            List<Project> projects = await GetProject.BuscarProjetosDoUsuarios(email);
            var convertido = new ProjectReturnBindingModel().Convert(projects);
            return Ok(convertido);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> Create(ProjectCreateBindingModel bindingModel) {

            Laboratory laboratory = await GetLaboratory.FindByIdAsync(bindingModel.IdLaboratory);

            Project project = new Project {
                Titulo = bindingModel.Titulo,
                Descricao = bindingModel.Descricao,
                Laboratory = laboratory,
                DataCriacao = DateTime.Now,
                DataFinalizacao = null,
            };

            laboratory.Adiciona(project);

            var atualizou = await GetLaboratory.Update(laboratory);
            return Ok();
        }
    }
}