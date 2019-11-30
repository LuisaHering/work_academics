using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebMVC.Models {
    public class PostViewModel {
        public string Id {
            get; set;
        }
        public string Autor {
            get; set;
        }
        public DateTime DataDePublicacao {
            get; set;
        }
        public string Mensagem {
            get; set;
        }
        public string UrlDocumento {
            get; set;
        }

    }
}