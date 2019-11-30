using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebApi.Models.Post {
    public class PostBindModel {
        public string Id {
            get; set;
        }

        public string Mensagem {
            get; set;
        }

        public string UrlDocumento {
            get; set;
        }

        public DateTime DataDePublicacao {
            get; set;
        }

        public string Autor {
            get; set;
        }

        public string NomeLaboratorio {
            get; set;
        }

        public List<PostBindModel> Convert(List<Core.Models.Post> posts) {
            List<PostBindModel> convertido = new List<PostBindModel>();

            foreach(Core.Models.Post post in posts) {
                PostBindModel model = new PostBindModel() {
                    Id = post.Id.ToString(),
                    Autor = post.Autor.Nome,
                    DataDePublicacao = post.DataPublicacao,
                    Mensagem = post.Mensagem,
                    UrlDocumento = post.UrlDocumento,
                    NomeLaboratorio = post.Laboratory.Descricao
                };
                convertido.Add(model);
            }
            return convertido;
        }
    }
}