using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SN_WebMVC.Models {
    public class ProfileViewModel {

        public string Id {
            get; set;
        }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public string Nome {
            get; set;
        }

        public string Foto {
            get; set;
        }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public string Email {
            get; set;
        }

        public DateTime Nascimento {
            get; set;
        }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public string Universidade {
            get; set;
        }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public string Curso {
            get; set;
        }

        public string DataInicio {
            get; set;
        }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        public string Biografia {
            get; set;
        }

        public List<ProfileViewModel> RemoverUsuarioLogado(string emailUsuarioLogado, List<ProfileViewModel> profiles) {
            List<ProfileViewModel> semUsuarioLogado = new List<ProfileViewModel>();
            foreach(ProfileViewModel profile in profiles) {
                if(!profile.Email.Contains(emailUsuarioLogado)) {
                    semUsuarioLogado.Add(profile);
                }
            }
            return semUsuarioLogado;
        }
    }
}