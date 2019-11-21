using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebApi.Models.Projeto {
    public class ProjectReturnBindingModel {

        public int Id {
            get; set;
        }

        public string Titulo {
            get; set;
        }

        public string Descricao {
            get; set;
        }

        public DateTime DataCriacao {
            get; set;
        }

        public List<ProjectReturnBindingModel> Convert(List<Project> projects) {
            List<ProjectReturnBindingModel> convertidos = new List<ProjectReturnBindingModel>();

            foreach(Project project in projects) {
                ProjectReturnBindingModel bindingModel = new ProjectReturnBindingModel {
                    Id = project.Id,
                    Titulo = project.Titulo,
                    Descricao = project.Descricao,
                    DataCriacao = project.DataCriacao
                };
                convertidos.Add(bindingModel);
            }
            return convertidos;
        }
    }
}