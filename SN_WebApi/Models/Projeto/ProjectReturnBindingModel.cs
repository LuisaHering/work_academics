using Core.Models;
using SN_WebApi.Models.Post;
using SN_WebApi.Models.Usuario;
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

        public LaboratoryReturnBindingModels laboratory {
            get; set;
        }

        public List<UserSimple> Membros {
            get; set;
        }

        public List<PostBindModel> Posts {
            get; set;
        }

        public ProjectReturnBindingModel() {
            laboratory = new LaboratoryReturnBindingModels();
            Membros = new List<UserSimple>();
            Posts = new List<PostBindModel>();
        }

        public ProjectReturnBindingModel Convert(Project project) {
            if(project != null) {
                var laboratory = new LaboratoryReturnBindingModels() {
                    Id = project.Laboratory.Id,
                    Descricao = project.Laboratory.Descricao
                };

                var projeto = new ProjectReturnBindingModel() {
                    Id = project.Id,
                    Descricao = project.Descricao,
                    Titulo = project.Titulo,
                    DataCriacao = project.DataCriacao,
                };

                var membros = new List<UserSimple>();

                var posts = new List<PostBindModel>();

                foreach(User u in project.Laboratory.Users) {
                    UserSimple simpleUser = new UserSimple();
                    simpleUser.Id = u.Id;
                    simpleUser.Nome = u.Nome;
                    simpleUser.Email = u.Email;
                    membros.Add(simpleUser);
                }

                foreach(Core.Models.Post p in project.Posts) {
                    PostBindModel post = new PostBindModel() {
                        Id = p.Id.ToString(),
                        Mensagem = p.Mensagem,
                        Autor = p.Autor.Nome,
                        UrlDocumento = p.UrlDocumento,
                        DataDePublicacao = p.DataPublicacao
                    };
                    posts.Add(post);
                }

                posts.Reverse();
                projeto.Posts = posts;
                projeto.Membros = membros;
                projeto.laboratory = laboratory;
                return projeto;
            }
            return null;
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