using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebApi.Models.Post {
    public class InputPostBindModel {

        public string Mensagem {
            get; set;
        }

        public string IdAutor {
            get; set;
        }

        public string UrlDocumento {
            get; set;
        }

        public int IdProjeto {
            get; set;
        }

        public int IdLaboratorio {
            get; set;
        }
    }
}