using Core.Models;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database = Data.Context.Database;

namespace Data.Services {
    public class ConexaoImpl : IConection {

        private UsersImpl UserService;

        public string connection_string {
            get; set;
        }

        public ConexaoImpl() {
            this.connection_string = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=aspnet-SN_WebApi-20191007082158;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            this.UserService = new UsersImpl();
        }

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
            List<Conection> conections = Database.GetInstance.Conection.ToList();
            Conection conection = null;

            foreach(Conection c in conections) {
                if(conexao.Seguido.Id.Equals(c.Seguido.Id) 
                    && conexao.Seguidor.Id.Equals(c.Seguidor.Id)) {
                    conection = c;
                    break;
                }
            }

            try {
                Database.GetInstance.Conection.Remove(conection);
                await Database.GetInstance.SaveChangesAsync();
                return true;
            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }

            return false;
        }

        public async Task<List<User>> Amigos(string idUsuario) {
            List<User> users = new List<User>();

            using(SqlConnection conn = new SqlConnection(connection_string)) {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Conexoes";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("id_user", idUsuario);
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while(dr.Read()) {
                    var user = new User() {
                        Id = new Guid(dr["Id"].ToString()),
                        Biografia = dr["Biografia"].ToString(),
                        Curso = dr["Curso"].ToString(),
                        DataInicio = Convert.ToDateTime(dr["DataInicio"].ToString()),
                        Email = dr["Email"].ToString(),
                        Nascimento = Convert.ToDateTime(dr["Nascimento"].ToString()),
                        Nome = dr["Nome"].ToString(),
                        Universidade = dr["Universidade"].ToString(),
                        UrlFoto = dr["UrlFoto"].ToString(),
                        Laboratories = null,
                        Pictures = null,
                        Role = null
                    };
                    users.Add(user);
                }
            }
            return users;
        }
    }
}
