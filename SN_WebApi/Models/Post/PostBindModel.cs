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
    }
}