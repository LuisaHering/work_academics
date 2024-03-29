﻿using Core.Models;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database = Data.Context.Database;

namespace Data.Services {

    public class ProjectImpl : IProject {

        private UsersImpl UserService = new UsersImpl();

        public async Task<Project> BuscaProjetoPor(int idDaBusca) {
            var projetos = await Database.GetInstance.Project.ToListAsync();
            
            foreach(Project p in projetos) {
                if(p.Id == idDaBusca) {
                    return p;
                }
            }
            return null;
        }

        public async Task<List<Project>> BuscarProjetosPor(string email) {
            User user = await UserService.FindByEmail(email);
            return user.Projects.ToList();
        }

        public async Task<bool> Create(Project project) {
            try {
                Database.GetInstance.Project.Add(project);
                await Database.GetInstance.SaveChangesAsync();
                return true;
            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public async Task<bool> Editar(Project project) {
            try {
                Database.GetInstance.Entry<Project>(project).State = EntityState.Modified;
                await Database.GetInstance.SaveChangesAsync();
                return true;
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        public async Task<bool> Update(Project projeto)
        {
            try
            {
                Database.GetInstance.Entry<Project>(projeto).State = EntityState.Modified;
                await Database.GetInstance.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
    }
}
