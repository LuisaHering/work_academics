using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services {
    public interface IPost {

        Task<bool> Postar(Post post);

        Task<List<Post>> Publicacoes(string idUser);
    }
}
