using Core.Models;
using Core.Services;
using Data.Services;
using SN_WebApi.Service;
using SN_WebApi.ServicosExternos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SN_WebApi.Controllers
{
    [RoutePrefix("api/upload")]
    public class UploadController : ApiController
    {
        private IUsers UsersService = ServiceLocator.GetInstanceOf<UsersImpl>();

        [HttpPost]
        [Route("foto")]
        public IHttpActionResult UploadFoto(HttpPostedFileBase foto, string email)
        {
            ServidorDeArquivos servidorDeArquivos = new ServidorDeArquivos();

            servidorDeArquivos.UploadDeArquivo(foto.InputStream, foto.FileName);

            var usuario = UsersService.FindByEmail(email);

            Picture fotoUsuario = new Picture();
            fotoUsuario.Url = "https://gabrielcouto26.blob.core.windows.net/teste/"+$"{foto.FileName}";
            fotoUsuario.User = usuario;

            return Ok();
        }
    }
}
