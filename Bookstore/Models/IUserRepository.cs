using System.Collections.Generic;

namespace Bookstore.Models
{
    public interface IUserRepository
    {            
        User Get(string UserId);
        //List<User> GetAll();
        //bool Insert(User user);
        //bool Update(User user);
        //bool Delete(int UserId);
        void Dispose();
    }
}
