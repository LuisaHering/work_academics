﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebApi.Models.Post {
    public class InputPostBindModel {

        public string Mensagem {
            get; set;
        }

        public string EmailUsuario {
            get; set;
        }

        public string UrlDocumento {
            get; set;
        }

        public string IdProjeto {
            get; set;
        }

        public string IdLaboratorio {
            get; set;
        }
    }
}