using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services {
    public interface IConection {

        Task<bool> Conectar(Conection conexao);

        Task<bool> Desconectar(Conection conexao);

        Task<List<Conection>> ListaConexoes(string idUsuario);
    }
}
