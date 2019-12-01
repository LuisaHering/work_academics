using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SN_WebMVC.Models {
    public class ProjectViewModel {

        public string Id {
            get; set;
        }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public string Titulo {
            get; set;
        }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public string Descricao {
            get; set;
        }

        public DateTime DataCriacao {
            get; set;
        }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public int IdLaboratory {
            get; set;
        }

        public List<ProfileViewModel> Membros {
            get; set;
        }
    }
}