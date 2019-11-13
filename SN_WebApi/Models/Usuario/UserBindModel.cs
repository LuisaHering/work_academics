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

        public UserBindModel() {

        }

        public UserBindModel Convert(User user) {
            if(user != null) {
                UserBindModel convertido = new UserBindModel();
                convertido.Id = user.Id;
                convertido.Nome = user.Nome;
                convertido.Foto = user.Foto;
                convertido.Email = user.Email;
                convertido.Universidade = user.Universidade;
                convertido.Curso = user.Curso;
                convertido.Nascimento = user.Nascimento;
                convertido.DataInicio = user.DataInicio;
                convertido.Biografia = user.Biografia;
                return convertido;
            }
            return null;
        }
    }
}