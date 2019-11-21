﻿using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services {
    public interface IProject {

        Task<List<Project>> BuscarProjetosDoUsuarios(string email);

        Task<bool> Create(Project project);
    }
}