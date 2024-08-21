using NguyenNhutDuy_2122110447.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NguyenNhutDuy_2122110447.Models
{
    public class CartModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int AvailableQuantity { get; set; }
    }
}