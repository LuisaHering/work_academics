using Core.Models;
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
        [HttpPost]
        [Route("foto")]
        public IHttpActionResult UploadFoto(HttpPostedFileBase foto, string user)
        {
            ServidorDeArquivos servidorDeArquivos = new ServidorDeArquivos();

            servidorDeArquivos.UploadDeArquivo(foto.InputStream, foto.FileName);

            //var usuario = UsersService.FindByEmail(user);

            Picture fotoUsuario = new Picture();
            fotoUsuario.Url = foto.FileName;

            return Ok();
        }
    }
}
