using EventBee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventBee.Repositories.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetById(int id);
        bool DeleteById(int id);
        User Update(int id, User updateUser);
        User Authenticate(string username, string password);
        User Register(User user);
    }
}
