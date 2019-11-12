using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebApi.Models.Projeto {
    public class ProjectReturnBindingModel {

        public string Titulo {
            get; set;
        }

        public string Descricao {
            get; set;
        }

        public DateTime DataCriacao {
            get; set;
        }

        public List<ProjectCreateBindingModel> Convert(List<Project> projetos) {
            return null;
        }
    }
}