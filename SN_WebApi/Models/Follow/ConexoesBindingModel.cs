using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebApi.Models.Follow
{
    public class ConexoesBindingModel
    {
        public string IdUsuario { get; set; }
        public List<User> Conexoes { get; set; }
    }
}