using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VendorWeb
{
    public static class SD
    {
        public static string BaseApiPath = "https://localhost:44383/";
        public static string userPath = BaseApiPath+ "api/User/";
        public static string productPath = BaseApiPath+"api/Product/";
        public static string resetPath = BaseApiPath + "api/reset/";
        public static string deposit = BaseApiPath + "api/deposit/";
        public static string shoppingCart = BaseApiPath + "api/ShoppingCart/";
    }
}
