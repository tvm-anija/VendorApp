using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Models.DTOs;

namespace WebApi.Mapper
{
    /// <summary>
    /// The Vendor mapping class
    /// </summary>
    public class VendorMapping : Profile
    {
        /// <summary>
        /// The vendor mapping constructor
        /// </summary>
        public VendorMapping()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
            CreateMap<User, UserCreateDto>().ReverseMap();
            CreateMap<ShoppingCart, ShoppingCartDto>().ReverseMap();
            CreateMap<Buy, BuyDto>().ReverseMap();
        }
    }
}
