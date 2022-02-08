﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendorWeb.Models;

namespace VendorWeb.Repository.IRepository
{
    public interface IRepository<T> where T:class
    {
        Task<T> GetAsync(string url, int Id, string token);
        Task<IEnumerable<T>> GetAllAsync(string url, string token);
        Task<bool> CreateAsync(string url, T objToCreate, string token);
        Task<bool> UpdateAsync(string url, T objToUpdate, string token);
        Task<bool> DeleteAsync(string url, int Id, string token);
        Task<bool> ResetAsync(string url, int Id, string token);
        Task<ShoppingCart> CreateShoppingCart(string url, T objToCreate, string token);
    }
}
