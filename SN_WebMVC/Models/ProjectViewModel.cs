using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebMVC.Models {
    public class ProjectViewModel {

        public string Id {
            get; set;
        }

        public string Titulo {
            get; set;
        }

        public string Descricao {
            get; set;
        }

        public DateTime DataCriacao {
            get; set;
        }

        public int IdLaboratory {
            get; set;
        }

        public List<ProfileViewModel> Membros {
            get; set;
        }
    }
}