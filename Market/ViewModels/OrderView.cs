using Market.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Market.ViewModels
{
    public class OrderView
    {
        public Customer Customer { get; set; }
        
        //Para los indices esclusivamente el ProductOrder
        public ProductOrder ProductOrder { get; set; }

        public List<ProductOrder> Products { get; set; }

    }
}