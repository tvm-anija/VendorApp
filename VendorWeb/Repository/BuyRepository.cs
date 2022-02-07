using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using VendorWeb.Models;
using VendorWeb.Repository.IRepository;

namespace VendorWeb.Repository
{
    public class BuyRepository : Repository<BuyDto>, IBuyRepository
    {
        private readonly IHttpClientFactory _clientFactory;
        public BuyRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
