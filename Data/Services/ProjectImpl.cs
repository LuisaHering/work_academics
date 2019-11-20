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
        public async Task<List<Project>> BuscarProjetosDoUsuarios(string email) {
            List<Project> projects = await Database.GetInstance.Project.ToListAsync();
            return projects;
        }
    }
}
