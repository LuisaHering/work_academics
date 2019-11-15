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
                Database.GetInstance.SaveChangesAsync();
                return true;
            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public List<Laboratory> FindByEmail(string userEmail) {
            List<Laboratory> labs = new List<Laboratory>();
            List<Laboratory> myLabs = new List<Laboratory>();

            labs.AddRange(Database.GetInstance.Laboratories.ToList());

            foreach(Laboratory lab in labs) {
                foreach(User user in lab.Users) {
                    if(user.Email == userEmail) {
                        myLabs.Add(lab);
                    }
                }
            }
            return myLabs;
        }

        public List<Laboratory> SearchLaboratoryBy(string description) {
            List<Laboratory> labs = new List<Laboratory>();
            var matches = Database.GetInstance.Laboratories.ToList();

            foreach(Laboratory lab in matches) {
                if(lab.Descricao.Contains(description)) {
                    labs.Add(lab);
                }
            }

            return labs;
        }
    }
}
