using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Models;
using WebApi.Repository.IRepository;

namespace WebApi.Repository
{
    /// <summary>
    /// The user repository
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// The user Repository constructor
        /// </summary>
        /// <param name="db">The DB context</param>
        public UserRepository(ApplicationDbContext db, IOptions<AppSettings> appSettings)
        {
            _db = db;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// To Create new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>bool</returns>
        public bool CreateUser(User user)
        {
            _db.users.Add(user);
            return Save();
        }

        /// <summary>
        /// To delete a user
        /// </summary>
        /// <param name="user">The user object</param>
        /// <returns>bool</returns>
        public bool DeleteUser(User user)
        {
            _db.users.Remove(user);
            return Save();
        }

        /// <summary>
        /// Get individual user by id
        /// </summary>
        /// <param name="id">The user id</param>
        /// <returns>The user object</returns>
        public User GetUser(int id)
        {
            return _db.users.FirstOrDefault(a => a.UserId == id);
        }

        /// <summary>
        /// The list of all users
        /// </summary>
        /// <returns>list of users from db</returns>
        public ICollection<User> GetUsers()
        {
            return _db.users.OrderBy(a => a.UserName).ToList();
        }

        /// <summary>
        /// Check whether the user exists or not by its name
        /// </summary>
        /// <param name="name">The user name</param>
        /// <returns>The user</returns>
        public bool UserExists(string name)
        {
            bool value = _db.users.Any(a => a.UserName.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        /// <summary>
        /// Check whether the user exists or not by its id
        /// </summary>
        /// <param name="id">The user id</param>
        /// <returns>bool</returns>
        public bool UserExists(int id)
        {
            return _db.users.Any(a => a.UserId == id);
        }

        /// <summary>
        /// The Save method
        /// </summary>
        /// <returns>bool</returns>
        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        /// <summary>
        /// Method to update the user
        /// </summary>
        /// <param name="user">The user object</param>
        /// <returns>bool</returns>
        public bool UpdateUser(User user)
        {
            _db.users.Update(user);
            return Save();
        }

        /// <summary>
        /// Method to reset the user balance
        /// </summary>
        /// <param name="user">The user object</param>
        /// <returns>bool</returns>
        public bool ResetDeposit(User user)
        {
            _db.users.Update(user);
            return Save();
        }

        bool IUserRepository.IsUniqueUser(string userName)
        {
            throw new NotImplementedException();
        }

        User IUserRepository.Authenticate(string userName, string password)
        {
            var user = _db.users.SingleOrDefault(x => x.UserName == userName && x.Password == password);

            // User not found
            if (user == null)
            {
                return null;
            }

            // if user was found generate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role,user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = "";
            return user;
        }
    }
}
