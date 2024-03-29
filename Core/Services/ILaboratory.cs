﻿using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services {
    public interface ILaboratory {
        Task<bool> Create(Laboratory laboratory);

        Task<bool> Update(Laboratory laboratory);

        Task<List<Laboratory>> FindByEmail(string userEmail);

        Task<Laboratory> FindByIdAsync(int id);

        List<Laboratory> SearchLaboratoryBy(string description);
    }
}
