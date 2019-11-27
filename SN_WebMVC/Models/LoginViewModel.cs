using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SN_WebMVC.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Email { get; set; }
    }
}