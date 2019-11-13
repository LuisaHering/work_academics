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

        // POST api/Account/Logout
        [Route("Logout")]
        [HttpGet]
        public IHttpActionResult Logout() {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        //// POST api/Account/ChangePassword
        //[Route("ChangePassword")]
        //public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model) {
        //    if(!ModelState.IsValid) {
        //        return BadRequest(ModelState);
        //    }

        //    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
        //        model.NewPassword);

        //    if(!result.Succeeded) {
        //        return GetErrorResult(result);
        //    }

        //    return Ok();
        //}

        [Route("change")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> ChangePassword(UserChangePassword changePassword) {

            return null;
        }
        
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model) {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

            if(!result.Succeeded) {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/RemoveLogin
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model) {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            IdentityResult result;

            if(model.LoginProvider == LocalLoginProvider) {
                result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
            } else {
                result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
                    new UserLoginInfo(model.LoginProvider, model.ProviderKey));
            }

            if(!result.Succeeded) {
                return GetErrorResult(result);
            }

            return Ok();
        }

        [AllowAnonymous]
        [Route("FindUser")]
        [HttpGet]
        public IHttpActionResult FindUserByEmail(string email) {
            UserBindModel usuario = new UserBindModel();
            usuario.Convert(UsersService.FindByEmail(email)); 

            if(usuario == null) {
                return BadRequest("Usuário não localizado");
            }

            return Ok(usuario);
        }

        // POST api/Account/Register
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
        public IHttpActionResult Update(UpdateBindingModel model) {

            var updatedUser = UsersService.FindByEmail(model.Email);
            ApplicationUser applicationUser = UserManager.FindByName(model.Email);
            var usuarioAux = false;

            if(updatedUser != null) {
                updatedUser.Biografia = model.Biografia;
                updatedUser.Nome = model.Nome;
                updatedUser.Universidade = model.Universidade;
                updatedUser.Curso = model.Curso;
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

        #region Helpers

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
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData {
            public string LoginProvider {
                get; set;
            }
            public string ProviderKey {
                get; set;
            }
            public string UserName {
                get; set;
            }

            public IList<Claim> GetClaims() {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if(UserName != null) {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity) {
                if(identity == null) {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if(providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value)) {
                    return null;
                }

                if(providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer) {
                    return null;
                }

                return new ExternalLoginData {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits) {
                const int bitsPerByte = 8;

                if(strengthInBits % bitsPerByte != 0) {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}
