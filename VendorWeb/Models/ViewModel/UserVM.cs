using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendorWeb.Models.ViewModel
{
    public class UserVM
    {
        public IEnumerable<SelectListItem> RoleList { get; set; }
        public User user { get; set; }
    }
}
