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
using SN_WebApi.Models.Usuario;
using SN_WebApi.Providers;
using SN_WebApi.Results;
using SN_WebApi.Service;

namespace SN_WebApi.Controllers {

    [Authorize]
    [RoutePrefix("api/post")]
    public class PostController : ApiController {

        private IUsers UsersService = ServiceLocator.GetInstanceOf<UsersImpl>();
        private IConection ConectionService = ServiceLocator.GetInstanceOf<ConexaoImpl>();
        private IPost PostService = ServiceLocator.GetInstanceOf<PostImpl>();

        [HttpGet]
        public async Task<IHttpActionResult> Index(DataToFollowing inputModel) {
            return Ok();
        }


        [HttpPost]
        public async Task<IHttpActionResult> Create(DataToFollowing inputModel) {
            return Ok();
        }

    }
}