using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Market.Models
{
    public class ProductOrder: Product
    {
        [DataType(DataType.Currency)]
        public float Quantity { get; set; }

        [DataType(DataType.Currency)]
        public decimal Value { get { return Price * (decimal)Quantity; } }
        

    }
}