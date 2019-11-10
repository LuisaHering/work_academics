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

        public bool Create(Laboratory laboratory) {
            try {
                Database.GetInstance.Laboratories.Add(laboratory);
                Database.GetInstance.SaveChanges();
                return true;
            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public List<Laboratory> FindAll(string userEmail) {
            List<Laboratory> list = new List<Laboratory>();
            list.AddRange(Database.GetInstance.Laboratories.ToList().Where(x => x.User.Email == userEmail));
            return list;
        }
    }
}
