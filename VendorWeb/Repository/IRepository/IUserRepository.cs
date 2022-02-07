using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendorWeb.Models;

namespace VendorWeb.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> LoginAsync(string url, Login objToCreate);
        Task<User> RegisterAsync(string url, User objToCreate);
    }
}
