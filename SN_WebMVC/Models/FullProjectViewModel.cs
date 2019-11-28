using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebMVC.Models {
    public class FullProjectViewModel {
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

        public List<string> Membros {
            get; set;
        }

        public List<string> Documentos {
            get; set;
        }

        public List<PostViewModel> Posts {
            get; set;
        }

        public FullProjectViewModel() {
            Membros = new List<string>();
            Posts = new List<PostViewModel>();
            Documentos = new List<string>();
        }
    }
}