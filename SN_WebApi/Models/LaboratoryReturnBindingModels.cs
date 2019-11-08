using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebApi.Models {
    public class LaboratoryReturnBindingModels {
        public int Id {
            get; set;
        }

        public string Descricao {
            get; set;
        }

        public List<LaboratoryReturnBindingModels> convert(List<Laboratory> laboratory) {
            List< LaboratoryReturnBindingModels> result = new List<LaboratoryReturnBindingModels>();

            foreach(Laboratory obj in laboratory) {
                LaboratoryReturnBindingModels aux = new LaboratoryReturnBindingModels() {
                    Id = obj.Id,
                    Descricao = obj.Descricao
                };
                result.Add(aux);
            }

            return result;
        }
    }
}