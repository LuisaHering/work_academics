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
using SN_WebApi.Models.ProfilePicture;
using SN_WebApi.Models.Usuario;
using SN_WebApi.Providers;
using SN_WebApi.Results;
using SN_WebApi.Service;
namespace SN_WebApi.Controllers {

    [RoutePrefix("api/picture")]
    public class PictureController : ApiController {

        private IPicture GetPicture = ServiceLocator.GetInstanceOf<PictureImpl>();

        [HttpGet]
        [Route("user")]
        public async Task<IHttpActionResult> FindLaboratory(string id_user) {

            List<Picture> pictures = await GetPicture.PicturesByUser(id_user);

            var retorno = new ProfilePictureBindModel().Convert(pictures);

            return Ok(retorno);
        }

    }
}