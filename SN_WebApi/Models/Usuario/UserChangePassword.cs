using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebApi.Models.Usuario {
    public class UserChangePassword {
        public string Email {
            get; set;
        }

        public string Password {
            get; set;
        }

        public string ConfirmPassword {
            get; set;
        }
    }
}