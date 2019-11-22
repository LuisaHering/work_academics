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
    public class PictureImpl : IPicture {
        public async Task<List<Picture>> PicturesByUser(string idUser) {
            List<Picture> pictures = await Database
                .GetInstance
                .Pictures
                .Where(p => p.User.Id.ToString().Equals(idUser))
                .ToListAsync();

            return pictures;
        }
    }
}
