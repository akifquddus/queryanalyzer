using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiR.Models;

namespace WebApiR.Helpers
{
    public interface IUserRepository
    {
        User Add(User user);

        User Update(User user);

        User GetUser(int userId);

        User GetUser(String username, String password);

        List<User> Get();

        bool DeleteUser(int userId);

        User UserExists(string email, string username);
    }
}
