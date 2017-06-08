using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Market.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }

        [DataType(DataType.Currency)]
        public float Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        

    }
}