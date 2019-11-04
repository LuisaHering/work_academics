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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {
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

        public string Universidade
        {
            get; set;
        }

        public string Curso
        {
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

        public User() {

        }
    }
}
