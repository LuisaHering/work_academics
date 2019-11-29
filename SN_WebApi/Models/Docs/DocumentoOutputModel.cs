using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebApi.Models.Docs {
    public class DocumentoOutputModel {
        public string Url {
            get; set;
        }

        public List<DocumentoOutputModel> Converter(Project project) {
            List<DocumentoOutputModel> lista = new List<DocumentoOutputModel>();

            foreach(Core.Models.Post p in project.Posts) {
                DocumentoOutputModel doc = new DocumentoOutputModel() {
                    Url = p.UrlDocumento
                };
                lista.Add(doc);
            }

            return lista;
        }
    }
}