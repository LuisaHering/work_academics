using SN_WebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebMVC.Service.Projeto {
    public class ProjetoService {

        public List<DocumentosViewModel> ListaDocumentos(ProjetoOutputModel projeto) {
            List<DocumentosViewModel> docs = new List<DocumentosViewModel>();

            foreach(PostViewModel p in projeto.Posts) {
                if(p.UrlDocumento != null) {
                    DocumentosViewModel doc = new DocumentosViewModel() {
                        Url = p.UrlDocumento
                    };
                    docs.Add(doc);
                }
            }
            return docs;
        }
    }
}