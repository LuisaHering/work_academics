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
    public class PostImpl : IPost {
        private UsersImpl UserService = new UsersImpl();
        private LaboratoryImpl LaboratoryService = new LaboratoryImpl();
        private ProjectImpl ProjectService = new ProjectImpl();

        public async Task<List<Post>> PicturesByUser(string idUser) {
            List<Post> posts = new List<Post>();

            using(SqlConnection conn = new SqlConnection("DefaultConnection")) {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Publicacoes";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while(dr.Read()) {
                    var post = new Post() {
                        Id = new Guid(dr["Id"].ToString()),
                        Autor = await UserService.FindById(dr["Autor_Id"].ToString()),
                        DataPublicacao = Convert.ToDateTime(dr["DataPublicacao"]),
                        Laboratory = await LaboratoryService.FindByIdAsync(Convert.ToInt32(dr["Laboratory_Id"])),
                        Mensagem = dr["Mensagem"].ToString(),
                        Projects = null,
                        UrlDocumento = dr["UrlDocumento"].ToString()
                    };
                    posts.Add(post);
                }

            }

            return posts;
        }

        public async Task<bool> Postar(Post post) {
            try {
                Database.GetInstance.Posts.Add(post);
                await Database.GetInstance.SaveChangesAsync();
                return true;
            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }
            return false;
        }
    }
}
