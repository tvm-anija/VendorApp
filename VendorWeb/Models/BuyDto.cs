using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendorWeb.Models
{
    public class BuyDto
    {
        public List<SelectedProducts> Products { get; set; }
        public int userId { get; set; }
        public int AmountAvailable { get; set; }
        public int Total { get; set; }

    }
}
