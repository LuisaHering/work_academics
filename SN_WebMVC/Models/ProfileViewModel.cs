using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebMVC.Models
{
    public class ProfileViewModel
    {
        public string Foto { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Universidade { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Biografia { get; set; }
        public string GrauAcademico { get; set; }
        public string CursoEmAndamento { get; set; }
        public string DataInicioCurso { get; set; } /*DateTime*/
        public string Laboratorios { get; set; }
        public string ProjetoEmAndamento { get; set; }
        public string ProjetosConcluidos { get; set; }
    }
}