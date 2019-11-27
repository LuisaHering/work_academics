using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SN_WebMVC.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$"
, ErrorMessage = "Informe um e-mail válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public string Universidade { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [RegularExpression(@"^.*(?=.{6,})(?=.*[!@#$%^&+=])(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).*$", ErrorMessage = "No minimo uma letra maiúscula, uma letra minuscula, um número e um carácter especial")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "É necessário confirmar a senha")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As senhas não coincidem")]
        public string ConfirmPassword { get; set; }

        public string Picture { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public DateTime Nascimento { get; set; }

        public string Curso { get; set; }

        public string Biografia { get; set; }

    }
}