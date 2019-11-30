using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebApi.Models.Follow
{
    public class ConnectionReturn
    {
        public string IdUsuario { get; set; }

        public List<ConnectionReturn> convert(List<Conection> listaConnections)
        {
            List<ConnectionReturn> result = new List<ConnectionReturn>();

            foreach (Conection obj in listaConnections)
            {
                ConnectionReturn aux = new ConnectionReturn()
                {
                    IdUsuario = obj.Seguidor.Id.ToString()
                };

                result.Add(aux);
            }

            return result;
        }
    }
}