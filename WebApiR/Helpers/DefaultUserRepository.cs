using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiR.Models;

namespace WebApiR.Helpers
{
    public class DefaultUserRepository : IUserRepository
    {
        private readonly WebApiRDbContext _context;
        public DefaultUserRepository(WebApiRDbContext context)
        {
            this._context = context;
        }

        public User Add(User user)
        {
            this._context.Users.Add(user);
            if (this._context.SaveChanges() != 1)
                throw new Exception("Unable to save user in Database.");

            return this.GetUser(user.Username, user.Password);
        }

        public bool DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public User GetUser(int userId)
        {
            var user = _context.Users.Find(userId);
            return user;
        }

        public User GetUser(string username, string password)
        {
            return this._context.Users.Where(_ => _.Username.Equals(username) && _.Password.Equals(password)).FirstOrDefault();
        }

        public User UserExists(string email, string username)
        {
            var user = this._context.Users.Where(_ => _.Email.Equals(email)).FirstOrDefault();
            if (user == null)
                return this._context.Users.Where(_ => _.Username.Equals(username)).FirstOrDefault();
            else
                return user;
        }

        public User Update(User user)
        {
            throw new NotImplementedException();
        }

        public List<User> Get()
        {
            return _context.Users.ToList();
        }
    }
}
