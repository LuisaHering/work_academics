using Core.Models;
using SN_WebApi.Models.Projeto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebApi.Models.Laboratorio {
    public class FullLaboratoryBindingModel {

        public int Id {
            get; set;
        }

        public string Descricao {
            get; set;
        }

        public List<UserBindModel> Users {
            get; set;
        }

        public List<ProjectReturnBindingModel> Projetos {
            get; set;
        }

        public FullLaboratoryBindingModel() {
            Users = new List<UserBindModel>();
            Projetos = new List<ProjectReturnBindingModel>();
        }

        public FullLaboratoryBindingModel Convert(Laboratory lab) {
            FullLaboratoryBindingModel aux = new FullLaboratoryBindingModel();
            aux.Id = lab.Id;
            aux.Descricao = lab.Descricao;

            foreach(Project p in lab.Projects) {
                var project = new ProjectReturnBindingModel() {
                    Id = p.Id,
                    Titulo = p.Titulo,
                    Descricao = p.Descricao,
                    DataCriacao = p.DataCriacao
                };
                aux.Projetos.Add(project);
            }

            foreach(User u in lab.Users) {
                var user = new UserBindModel() {
                    Id = u.Id,
                    Biografia = u.Biografia,
                    Curso = u.Curso,
                    DataInicio = u.DataInicio,
                    Email = u.Email,
                    Foto = u.UrlFoto,
                    Nascimento = u.Nascimento,
                    Nome = u.Nome,
                    Universidade = u.Universidade
                };

                aux.Users.Add(user);
            }
            return aux;
        }
    }
}