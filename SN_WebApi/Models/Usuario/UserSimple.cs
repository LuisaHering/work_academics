using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebApi.Models.Usuario {
    public class UserSimple {

        public Guid Id {
            get; set;
        }

        public string Nome {
            get; set;
        }

        public string Email {
            get; set;
        }
    }
}