using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebApi.Models.Projeto
{
    public class EntrarNoProjeto
    {
        public string IdUsuario
        {
            get; set;
        }

        public int IdProjeto
        {
            get; set;
        }
    }
}