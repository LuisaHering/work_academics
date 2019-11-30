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
using System.Web.UI.WebControls;
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
using SN_WebApi.Models.Follow;
using SN_WebApi.Models.Post;
using SN_WebApi.Models.Usuario;
using SN_WebApi.Providers;
using SN_WebApi.Results;
using SN_WebApi.Service;

namespace SN_WebApi.Controllers {

    [Authorize]
    [RoutePrefix("api/post")]
    public class PostController : ApiController {

        private IUsers UsersService = ServiceLocator.GetInstanceOf<UsersImpl>();
        private IPost GetPost = ServiceLocator.GetInstanceOf<PostImpl>();
        private ILaboratory GetLaboratory = ServiceLocator.GetInstanceOf<LaboratoryImpl>();
        private IProject GetProject = ServiceLocator.GetInstanceOf<ProjectImpl>();

        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Index(string iduser) {
            var publicacoes = await GetPost.Publicacoes(iduser);
            var retorno = new PostBindModel().Convert(publicacoes);
            return Ok(retorno);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Create(InputPostBindModel inputModel) {

            var usuario = await UsersService.FindByEmail(inputModel.EmailUsuario);
            var laboratorio = await GetLaboratory.FindByIdAsync(Convert.ToInt32(inputModel.IdLaboratorio));
            var projeto = (Project)await GetProject.BuscaProjetoPor(Convert.ToInt32(inputModel.IdProjeto));

            var novo_post = new Post().CriarPost(inputModel.Mensagem, usuario, inputModel.UrlDocumento, laboratorio);
            projeto.Posts.Add(novo_post);

            var post_salvo = await GetPost.Postar(novo_post);

            var editou = await GetProject.Editar(projeto);

            if(post_salvo && editou) {
                return Ok();
            }

            return BadRequest("Erro ao editar ou salvar o projeto");
        }
    }
}