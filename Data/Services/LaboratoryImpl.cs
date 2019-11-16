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
    public class LaboratoryImpl : ILaboratory {

        public async Task<bool> Create(Laboratory laboratory) {
            try {
                Database.GetInstance.Laboratories.Add(laboratory);
                int id = await Database.GetInstance.SaveChangesAsync();
                return true;
            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public async Task<List<Laboratory>> FindByEmail(string userEmail) {
            List<Laboratory> labs = await Database.GetInstance.Laboratories.ToListAsync();
            List<Laboratory> myLabs = new List<Laboratory>();

            //labs.AddRange(Database.GetInstance.Laboratories.ToList());

            foreach(Laboratory lab in labs) {
                foreach(User user in lab.Users) {
                    if(user.Email == userEmail) {
                        myLabs.Add(lab);
                    }
                }
            }
            return myLabs;
        }

        public async Task<Laboratory> FindByIdAsync(int id) {
            List<Laboratory> list = await Database.GetInstance.Laboratories.ToListAsync();

            foreach(Laboratory laboratory in list) {
                if(laboratory.Id == id) {
                    return laboratory;
                }
            }
           return null;
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
