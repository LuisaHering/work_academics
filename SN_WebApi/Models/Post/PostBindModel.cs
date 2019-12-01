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

        public string ImgAutor {
            get; set;
        }
        public string Autor {
            get; set;
        }

        public int IdLaboratorio {
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
                    ImgAutor = post.Autor.UrlFoto,
                    DataDePublicacao = post.DataPublicacao,
                    Mensagem = post.Mensagem,
                    UrlDocumento = post.UrlDocumento,
                    IdLaboratorio = post.Laboratory.Id,
                    NomeLaboratorio = post.Laboratory.Descricao
                };
                convertido.Add(model);
            }
            return convertido;
        }
    }
}