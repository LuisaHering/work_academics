using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SN_WebMVC.Models {
    public class LaboratoryViewModel {

        public int Id {
            get; set;
        }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public string Descricao {
            get; set;
        }

        public LaboratoryViewModel() {

        }
    }
}