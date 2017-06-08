using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Market.Models
{
    public class Product
    {
        
        [Key]
        public int ProductID { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime LastBuy { get; set; }
        public float Stock { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        public virtual ICollection<SupplierProduct> SupplierProducts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}