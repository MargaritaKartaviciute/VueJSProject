using EventBee.Data;
using EventBee.Helpers;
using EventBee.Models;
using EventBee.Models.Users;
using EventBee.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EventBee.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly EventBeeContext _context;
        private readonly AppSettings _appSettings;

        public UserRepository(IOptions<AppSettings> appSettings, EventBeeContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public List<User> GetAll()
        {
            var values = _context.Users.ToList();
            return values;
        }
        public User GetById(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            if (user != null)
                user.Password = null;

            return user;
        }

        public bool DeleteById(int id)
        {
            var user = _context.Users.SingleOrDefault(x => x.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return true;
            }
            return false;
            
        }

        public User Update(int id, User updateUser)
        {
            var findUser = _context.Users.SingleOrDefault(x => x.Id == id);
            if (findUser != null)
            {
                findUser.FirstName = updateUser.FirstName;
                findUser.Surname = updateUser.Surname;
                findUser.Role = updateUser.Role;
                findUser.Password = updateUser.Password;

                _context.Update(findUser);
                _context.SaveChanges();

                return findUser;
            }
            return null;            
        }

        public User Authenticate(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == username && x.Password == Password.hashPassword(password + _appSettings.Salt));

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            user.Password = null;

            return user;
        }
        public User Register(User user)
        {
            var exist_user = _context.Users.Any(x => x.Username == user.Username);
            if (exist_user == true)
            {
                return null;
            }
            user.Password = Password.hashPassword(user.Password + _appSettings.Salt);
            user.Role = Role.User;
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
    }
}
