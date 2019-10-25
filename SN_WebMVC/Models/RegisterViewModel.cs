using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebMVC.Models
{
    public class RegisterViewModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Universidade { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Picture { get; set; }
        public string Name { get; set; }
        public DateTime Nascimento { get; set; }
        public string Curso { get; set; }
        public string Biografia { get; set; }

    }
}