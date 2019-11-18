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
    public class UsersImpl : IUsers {

        public bool Create(User user) {

            try {
                Database.GetInstance.Users.Add(user);
                Context.Database.GetInstance.SaveChangesAsync();
                return true;
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            return false;
        }

        public async Task<User> FindById(string user_id) {
            User user = null;

            List<User> users = await Database.GetInstance.Users.Where(x => x.Id.ToString().Equals(user_id)).ToListAsync();

            foreach(User u in users) {
                if(u.Id.ToString().Equals(user_id)) {
                    user = u;
                    break;
                }
            }
            return user;
        }

        public async Task<User> FindByEmail(string email) {
            List<User> users = await Database.GetInstance.Users.Where(x => x.Email == email).ToListAsync();

            User localizado = null;

            if(users.Count > 0) {
                foreach(User u in users) {
                    if(u.Email == email) {
                        localizado = u;
                        break;
                    }
                }
            }
            return localizado;
        }

        public async Task<List<User>> FindUsersByName(string name) {
            List<User> users = await Database.GetInstance.Users.Where(x => x.Nome.Contains(name)).ToListAsync();
            return users;
        }

        public bool UpdateEF2(User user) {
            try {
                Database.GetInstance.Entry<User>(user).State = EntityState.Modified;
                Database.GetInstance.SaveChangesAsync();
                return true;
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        public bool Update(User user) {

            /*
                Converte data tipo datetime c# para date do tipo sql
             */
            string inicio = $"{user.DataInicio.Day}-{user.DataInicio.Month}-{user.DataInicio.Year}";
            string nascimento = $"{user.Nascimento.Day}-{user.Nascimento.Month}-{user.Nascimento.Year}";

            /*
                Query de atualização
             */
            string query = $"Update dbo.Users Set " +
                            $"Nome = '{user.Nome}', " +
                            $"Foto = '{user.UrlFoto}', " +
                            $"Email = '{user.Email}', " +
                            $"Universidade = '{user.Universidade}', " +
                            $"Curso = '{user.Curso}', " +
                            $"Nascimento = {nascimento}, " +
                            $"DataInicio = {inicio}, " +
                            $"Biografia = '{user.Biografia}' " +
                            $"where Email = '{user.Email}' ";

            try {
                Database
                    .GetInstance
                    .Database
                    .ExecuteSqlCommand(query);
                return true;
            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }
            return false;
        }
    }
}
