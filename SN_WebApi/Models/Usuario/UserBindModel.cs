using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebApi.Models {
    public class UserBindModel {

        public Guid Id {
            get; set;
        }

        public string Nome {
            get; set;
        }

        public string Foto {
            get; set;
        }

        public string Email {
            get; set;
        }

        public string Universidade {
            get; set;
        }

        public string Curso {
            get; set;
        }

        public DateTime Nascimento {
            get; set;
        }

        public DateTime DataInicio {
            get; set;
        }

        public string Biografia {
            get; set;
        }

        public UserBindModel Convert(User user) {
            UserBindModel convertido = new UserBindModel();
            this.Id = user.Id;
            this.Nome = user.Nome;
            this.Foto = user.Foto;
            this.Email = user.Email;
            this.Universidade = user.Universidade;
            this.Curso = user.Curso;
            this.Nascimento = user.Nascimento;
            this.DataInicio = user.DataInicio;
            this.Biografia = user.Biografia;
            return convertido;
        }
    }
}