using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebApi.Models.Follow {
    public class ConnectionReturn {
        public string IdUsuario {
            get; set;
        }

        public List<ConnectionReturn> convert(List<Conection> conexoes) {
            List<ConnectionReturn> result = new List<ConnectionReturn>();

            foreach(Conection obj in conexoes) {
                ConnectionReturn aux = new ConnectionReturn() {
                    IdUsuario = obj.Seguidor.Id.ToString()
                };

                result.Add(aux);
            }

            return result;
        }
    }
}