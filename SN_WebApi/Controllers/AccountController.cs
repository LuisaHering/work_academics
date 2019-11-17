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
using SN_WebApi.Models.Usuario;
using SN_WebApi.Providers;
using SN_WebApi.Results;
using SN_WebApi.Service;

namespace SN_WebApi.Controllers {
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController {

        private const string LocalLoginProvider = "Local";

        private ApplicationUserManager _userManager;

        private IUsers UsersService = ServiceLocator.GetInstanceOf<UsersImpl>();

        public AccountController() {
        }

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat) {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager {
            get {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat {
            get; private set;
        }

        [Route("Logout")]
        [HttpGet]
        public IHttpActionResult Logout() {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        [Route("change")]
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult ChangePassword(UserChangePassword changePassword) {
            ApplicationUser applicationUser = UserManager.FindByName(changePassword.Email);

            if(applicationUser != null) {
                UserManager.RemovePassword(applicationUser.Id);
                var result = UserManager.AddPassword(applicationUser.Id, changePassword.Password);

                if(result.Succeeded) {
                    return Ok();
                }
            }
            return BadRequest("Erro interno");
        }

        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model) {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser() {
                UserName = model.Email,
                Email = model.Email
            };

            var usuario = new User() {
                Nome = model.Name,
                Email = model.Email,
                DataInicio = DateTime.Now,
                Nascimento = DateTime.Now,
                Universidade = model.University
            };

            IdentityResult result = null;
            bool created = false;
            try {
                result = await UserManager.CreateAsync(user, model.Password);
                usuario.Id = new Guid(user.Id);
            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }

            created = UsersService.Create(usuario);

            if(!result.Succeeded || !created) {
                return GetErrorResult(result);
            }

            return Ok();
        }

        [Route("update")]
        [HttpPut]
        public async Task<IHttpActionResult> Update(UpdateBindingModel model) {

            var updatedUser = await UsersService.FindByEmail(model.Email);
            ApplicationUser applicationUser = UserManager.FindByName(model.Email);
            var usuarioAux = false;

            if(updatedUser != null) {
                updatedUser.Biografia = model.Biografia;
                updatedUser.Nome = model.Nome;
                updatedUser.Universidade = model.Universidade;
                updatedUser.Curso = model.Curso;
                updatedUser.Email = model.Email;
                updatedUser.setUrlFoto(model.CodeIMG);

                Picture picture = new Picture();
                picture.Url = updatedUser.getUrlFoto();
                picture.User = updatedUser;

                updatedUser.Pictures.Add(picture);

                usuarioAux = UsersService.UpdateEF2(updatedUser);
                /////////////////////////////////////////////////
                applicationUser.Email = model.Email;
                applicationUser.UserName = model.Email;
            } else {
                return BadRequest("Erro ao atualizar os dados do usuario");
            }

            var usuarioReal = UserManager.Update(applicationUser);

            if(!usuarioAux || !usuarioReal.Succeeded) {
                return BadRequest("Erro ao atualizar os dados do usuario");
            }
            return Ok("Atualizado com sucesso");
        }

        protected override void Dispose(bool disposing) {
            if(disposing && _userManager != null) {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        private IAuthenticationManager Authentication {
            get {
                return Request.GetOwinContext().Authentication;
            }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result) {
            if(result == null) {
                return InternalServerError();
            }

            if(!result.Succeeded) {
                if(result.Errors != null) {
                    foreach(string error in result.Errors) {
                        ModelState.AddModelError("", error);
                    }
                }

                if(ModelState.IsValid) {
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
