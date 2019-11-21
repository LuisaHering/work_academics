using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebApi.Models.Projeto {
    public class ProjectCreateBindingModel {
        public string Titulo {
            get; set;
        }

        public string Descricao {
            get; set;
        }

        public int IdLaboratory {
            get; set;
        }
    }
}