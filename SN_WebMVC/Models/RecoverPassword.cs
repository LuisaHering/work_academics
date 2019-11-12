using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebMVC.Models {
    public class RecoverPassword {

        public string Email {
            get; set;
        }

        public string Senha {
            get; set;
        }

        public string ConfirmarSenha {
            get; set;
        }
    }
}