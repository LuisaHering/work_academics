using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebMVC.Models {
    public class FullLaboratory {

        public int Id {
            get; set;
        }

        public string Descricao {
            get; set;
        }

        public List<ProjectViewModel> Projetos {
            get; set;
        }

        public List<ProfileViewModel> Users {
            get; set;
        }

        public FullLaboratory() {
            Users = new List<ProfileViewModel>();
            Projetos = new List<ProjectViewModel>();
        }
    }
}