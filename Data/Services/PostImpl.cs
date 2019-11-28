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
    public class PostImpl : IPost {
        public async Task<bool> Postar(Post post) {
            try {
                Database.GetInstance.Posts.Add(post);
                await Database.GetInstance.SaveChangesAsync();
                return true;
            } catch(Exception e) {
                Console.WriteLine(e.Message);
            }
            return false;
        }
    }
}
