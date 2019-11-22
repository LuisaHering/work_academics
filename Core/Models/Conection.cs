using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models {
    public class Conection {

        public Guid Id {
            get; set;
        }

        public User Seguidor {
            get; set;
        }

        public User Seguido {
            get; set;
        }

        public Conection() {

        }

        public Conection(User seguidor, User seguido) {
            this.Id = Guid.NewGuid();
            this.Seguidor = seguidor;
            this.Seguido = seguido;
        }

        public Conection Conectar(User seguidor, User seguido) {
            return new Conection(seguidor, seguido);
        }
    }
}
