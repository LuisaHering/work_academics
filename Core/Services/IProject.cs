using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services {
    public interface IProject {

        Task<List<Project>> BuscarProjetosPor(string email);

        Task<Project> BuscaProjetoPor(int id);

        Task<bool> Create(Project project);

        Task<bool> Editar(Project project);

        Task<bool> Update(Project project);

    }
}
