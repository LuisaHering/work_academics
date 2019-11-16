using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models {
    public class User {

        [Key]
        public Guid Id {
            get; set;
        }

        public string Nome {
            get; set;
        }

        public string UrlFoto {
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

        public virtual ICollection<Laboratory> Laboratories {
            get; set;
        }

        public virtual ICollection<Picture> Pictures
        {
            get; set;
        }

        public virtual Role Role {
            get; set;
        }

        public User() {
            Laboratories = new HashSet<Laboratory>();
            Pictures = new HashSet<Picture>();
        }

        public void Adiciona(Laboratory laboratory) {
            this.Laboratories.Add(laboratory);
        }

        public bool haveRole() {
            return this.Role != null;
        }

        public void setUrlFoto(string code) {
            this.UrlFoto = @"https://bankcarlos.blob.core.windows.net/api-amigo-fotos/" + code+".png";
        }
    }
}
