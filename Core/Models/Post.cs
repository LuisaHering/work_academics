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

        // tabela muitos para muitos
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
            var projects = new List<Project>();
        }

        public Post CriarPost(string mensagem, User autor, string idDocumento, Laboratory laboratory) {

            var post = new Post() {
                Id = Guid.NewGuid(),
                Autor = autor,
                Laboratory = laboratory,
                DataPublicacao = DateTime.Now,
                Mensagem = mensagem,
                UrlDocumento = SetUrlDocumento(idDocumento)
            };
            return post;
        }

        private string SetUrlDocumento(string IdDocumento) {
            return @"https://gabrielcouto26.blob.core.windows.net/api-amigo-fotos/" + IdDocumento + ".pdf";
        }
    }
}
