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

        public DateTime DataPublicacao {
            get; set;
        }

        public string NomeAutor {
            get; set;
        }
    }
}