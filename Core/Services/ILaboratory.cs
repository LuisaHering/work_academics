using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services {
    public interface ILaboratory {
        bool Create(Laboratory laboratory);

        List<Laboratory> FindByEmail(string userEmail);

        List<Laboratory> SearchLaboratoryBy(string description);
    }
}
