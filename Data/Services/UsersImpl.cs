using Core.Models;
using Core.Services;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services {
    public class UsersImpl : IUsers {

        public bool Create(User user) {

            try {
                Teste.db.Users.Add(user);
                Teste.db.SaveChangesAsync();
                //database.Users.Add(user);
                //database.SaveChangesAsync();
                return true;
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            return false;
        }

        public User FindByEmail(string email) {
            return Teste.db.Users.Where(x => x.Email == email).FirstOrDefault();
            //return database.Users.Where(x => x.Email == email).FirstOrDefault();
        }

        public bool UpdateEF2(User user) {
            try {
                Teste.db.Entry<User>(user).State = EntityState.Modified;
                Teste.db.SaveChangesAsync();
                //database.Entry<User>(user).State = EntityState.Modified;
                //database.SaveChanges();
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
                            $"Foto = '{user.Foto}', " +
                            $"Email = '{user.Email}', " +
                            $"Universidade = '{user.Universidade}', " +
                            $"Curso = '{user.Curso}', " +
                            $"Nascimento = {nascimento}, " +
                            $"DataInicio = {inicio}, " +
                            $"Biografia = '{user.Biografia}' " +
                            $"where Email = '{user.Email}' ";

            try {
                Teste
                    .db
                //database
                    .Database
                    .ExecuteSqlCommand(query);
                return true;
            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        //public bool Create(Laboratory laboratory) {
        //    try {
        //        //database.Laboratories.Add(laboratory);
        //        database.SaveChanges();
        //        return true;
        //    } catch(Exception e) {
        //        Console.WriteLine(e.Message);
        //    }

        //    return false;
        //}
    }
}
