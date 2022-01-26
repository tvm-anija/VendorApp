using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repository.IRepository
{
    /// <summary>
    /// The user repository interface
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Method to get all user
        /// </summary>
        /// <returns>List of users</returns>
        ICollection<User> GetUsers();

        /// <summary>
        /// Method to get individual user
        /// </summary>
        /// <param name="id">The user id</param>
        /// <returns>The user objec</returns>
        User GetUser(int id);

        /// <summary>
        /// Check whether the user exists or not based on name
        /// </summary>
        /// <param name="name">The user name</param>
        /// <returns>bool</returns>
        bool UserExists(string name);

        /// <summary>
        /// Check whether the user exists or not based on id
        /// </summary>
        /// <param name="id">The user id</param>
        /// <returns>bool</returns>
        bool UserExists(int id);

        /// <summary>
        /// To create a new user
        /// </summary>
        /// <param name="user">The user object</param>
        /// <returns>bool</returns>
        bool CreateUser(User user);

        /// <summary>
        /// To update a user
        /// </summary>
        /// <param name="user">The user object</param>
        /// <returns>bool</returns>
        bool UpdateUser(User user);

        /// <summary>
        /// To delete a user
        /// </summary>
        /// <param name="user">The user object</param>
        /// <returns>bool</returns>
        bool DeleteUser(User user);

        /// <summary>
        /// Method to save a user
        /// </summary>
        /// <returns></returns>
        bool Save();

        /// <summary>
        /// To uniquely identify the user
        /// </summary>
        /// <param name="userName">The user name</param>
        /// <returns>bool</returns>
        bool IsUniqueUser(string userName);
        /// <summary>
        /// Authentication method
        /// </summary>
        /// <param name="userName">The user name</param>
        /// <param name="password">The password</param>
        /// <returns>The user instance</returns>
        User Authenticate(string userName, string password);
    }
}
