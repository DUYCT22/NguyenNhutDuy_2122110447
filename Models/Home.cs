using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NguyenNhutDuy_2122110447.Context;
namespace NguyenNhutDuy_2122110447.Models
{
    public class Home
    {   public List<Product> ListProduct { get; set; }
        public List<Category> ListCategory { get; set; }
        public List<Product> ListProByCate { get; set; }
        public List<Product> ListProByCate2 { get; set; }
    }
}