using Core.Models;
using Core.Services;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services {
    public class LaboratoryImpl : ILaboratory {

        private DatabaseContext database = new DatabaseContext();


        public bool Create(Laboratory laboratory) {
            

            throw new NotImplementedException();
        }
    }
}
