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

    public class ProjectImpl : IProject {
        public Project BuscaProjetoPor(int idDaBusca) {
            //var projeto = (Project)Database.GetInstance.Project.ToList().Where(x => x.Id == id);

            var projetos = Database.GetInstance.Project.ToList();

            foreach(Project p in projetos) {
                if(p.Id == idDaBusca) {
                    return p;
                }
            }
            return null;
        }

        public async Task<List<Project>> BuscarProjetosPor(string email) {
            List<Project> allProjects = await Database.GetInstance.Project.ToListAsync();

            List<Project> myProjects = new List<Project>();

            foreach(Project project in allProjects) {
                foreach(User user in project.Laboratory.Users) {
                    if(user.Email.Equals(email)) {
                        myProjects.Add(project);
                    }
                }
            }
            return myProjects;
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
    }
}
