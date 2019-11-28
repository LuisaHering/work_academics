using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models {
    public class Post {

        public Guid Id {
            get; set;
        }

        public string Mensagem {
            get; set;
        }

        public User Autor {
            get; set;
        }

        public string UrlDocumento {
            get; set;
        }

        public virtual List<Project> Projects {
            get; set;
        }

        public virtual Laboratory Laboratory {
            get; set;
        }

        public DateTime DataPublicacao {
            get; set;
        }

        public Post() {
            var posts = new List<Project>();
        }

        public Post CriarPost(string mensagem, User autor, string urlDocumento, Project projeto, Laboratory laboratory, DateTime datapublicacao) {
            var projetos = new List<Project>();
            projetos.Add(projeto);

            var post = new Post() {
                Id = Guid.NewGuid(),
                Autor = autor,
                Laboratory = laboratory,
                Projects = projetos,
                DataPublicacao = DateTime.Now,
                Mensagem = mensagem,
                UrlDocumento = urlDocumento
            };
            return post;
        }
    }
}
