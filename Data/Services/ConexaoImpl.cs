using Core.Models;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database = Data.Context.Database;

namespace Data.Services {
    public class ConexaoImpl : IConection {
        public async Task<bool> Conectar(Conection conexao) {
            try {
                Database.GetInstance.Conection.Add(conexao);
                await Database.GetInstance.SaveChangesAsync();
                return true;
            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public async Task<bool> Desconectar(Conection conexao) {
            try {
                Database.GetInstance.Conection.Remove(conexao);
                await Database.GetInstance.SaveChangesAsync();
                return true;
            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }
            return false;
        }
    }
}
