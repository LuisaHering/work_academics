﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SN_WebMVC.Models {
    public class ProjetoOutputModel {

        public ProjetoOutputModel() {
            Membros = new List<MembrosOutputModel>();
            Posts = new List<PostViewModel>();
        }

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

        public LaboratoryViewModel laboratory {
            get; set;
        }

        public List<MembrosOutputModel> Membros {
            get; set;
        }

        public List<PostViewModel> Posts {
            get; set;
        }

        [Required(ErrorMessage ="Teste")]
        public string Post {
            get; set;
        }
    }
}