using ForTheApplication.Data;
using ForTheApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForTheApplication.Services
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext _context;

        public UserService(DatabaseContext context)
        {
            _context = context;
        }
        
        public IEnumerable<UserModel> AllUsers()
        {
            return _context.Users
                .Select(x => new UserModel
                {
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber
                });
        }

        public UserModel AuthenticateUser(string email, string password)
        {
            var model = _context.Users
            .Select(x => new UserModel
            {
                Email = x.Email,
                Password = x.Password,
                PhoneNumber = x.PhoneNumber
            })
            .FirstOrDefault(x => x.Email == email && x.Password == password);

            return model;
        }

        public UserModel CreateUser(UserModel newuser)
        {         
            _context.Add(newuser);

            _context.SaveChangesAsync();

            return newuser;

        }

        public UserModel FindUser(string email)
        {
            var model = _context.Users
            .Select(x => new UserModel
            {
                Email = x.Email
            })
            .FirstOrDefault(x => x.Email == email);

            return model;
        }
    }

    public interface IUserService
    {
        IEnumerable<UserModel> AllUsers();
        UserModel FindUser(string email);

        UserModel AuthenticateUser(string email, string password);

        UserModel CreateUser(UserModel newuser);
    }
}
