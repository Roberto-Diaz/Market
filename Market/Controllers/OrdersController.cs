using Market.Models;
using Market.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Market.Controllers
{
    public class OrdersController : Controller
    {
        MarketContext db = new MarketContext();
        // GET: Orders
        public ActionResult NewOrder()
        {
            var orderView = new OrderView();
            orderView.Customer = new Customer();
            orderView.Products = new List<ProductOrder>();

            Session["orderView"] = orderView;

            var list = db.Customers.ToList();
            list.Add(new Customer { CustomerID = 0, FirstName = "[Selecciona un cliente...]" });
            list = list.OrderBy(c => c.FullName).ToList();
            ViewBag.CustomerID = new SelectList(list, "CustomerID", "FullName");

            return View(orderView);
        }

        [HttpPost]
        public ActionResult NewOrder(OrderView orderView)
        {
            orderView = Session["orderView"] as OrderView;
            var CustomerID = int.Parse(Request["CustomerID"]);

            if (CustomerID == 0)
            {
                var list = db.Customers.ToList();
                list.Add(new Customer { CustomerID = 0, FirstName = "[Selecciona un cliente...]" });
                list = list.OrderBy(c => c.FullName).ToList();
                ViewBag.CustomerID = new SelectList(list, "CustomerID", "FullName");
                ViewBag.Error = "Debe seleccionar un cliente";

                return View(orderView);

            }

            var customer = db.Customers.Find(CustomerID);

            if (customer == null)
            {
                return View(orderView);
            }

            
            if (orderView.Products.Count == 0)
            {
                var list = db.Customers.ToList();
                list.Add(new Customer { CustomerID = 0, FirstName = "[Selecciona un cliente...]" });
                list = list.OrderBy(c => c.FullName).ToList();
                ViewBag.CustomerID = new SelectList(list, "CustomerID", "FullName");
                ViewBag.Error = "Debe ingresa un detalle";
                return View(orderView);
            }

            int orderID = 0;
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var order = new Order
                    {
                        CustomerID = CustomerID,
                        DateOrder = DateTime.Now,
                        OrderStatus = OrderStatus.Creat
                    };
                    db.Orders.Add(order);
                    db.SaveChanges();


                    orderID = db.Orders.ToList().Select(o => o.OrderID).Max();

                    foreach (var item in orderView.Products)
                    {
                        var orderDetail = new OrderDetail
                        {
                            OrderID = orderID,
                            Description = item.Description,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            ProductID = item.ProductID
                        };
                        db.OrderDetails.Add(orderDetail);
                    }
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ViewBag.Error = "Error: "+ ex.Message;
                    var listB = db.Customers.ToList();
                    listB.Add(new Customer { CustomerID = 0, FirstName = "[Selecciona un cliente...]" });
                    listB = listB.OrderBy(c => c.FullName).ToList();
                    ViewBag.CustomerID = new SelectList(listB, "CustomerID", "FullName");

                    orderView = new OrderView();
                    orderView.Customer = new Customer();
                    orderView.Products = new List<ProductOrder>();
                    Session["orderView"] = orderView;
                    return View(orderView);
                }
                
            }
                ViewBag.Message = string.Format("La orden {0} se guardo correctamente", orderID);

                var listC = db.Customers.ToList();
                listC.Add(new Customer { CustomerID = 0, FirstName = "[Selecciona un cliente...]" });
                listC = listC.OrderBy(c => c.FullName).ToList();
                ViewBag.CustomerID = new SelectList(listC, "CustomerID", "FullName");

                orderView = new OrderView();
                orderView.Customer = new Customer();
                orderView.Products = new List<ProductOrder>();
                Session["orderView"] = orderView;
                return View(orderView);
            }


        public ActionResult AddProduct()
        {
            var list = db.Products.ToList();
            list.Add(new Product { ProductID = 0, Description = "[Selecciona un producto...]" });
            list = list.OrderBy(p => p.Description).ToList();
            ViewBag.ProductID = new SelectList(list, "ProductID", "Description");

            return View();

        }

        [HttpPost]
        public ActionResult AddProduct(ProductOrder productOrder)
        {
            //Explicitamente se realiza un casting para saber a que objeto se esta refiriendo.
            var orderView = Session["orderView"] as OrderView;

            var productID = int.Parse(Request["ProductID"]);

            if (productID == 0)
            {
                var list = db.Products.ToList();
                list.Add(new Product { ProductID = 0, Description = "[Selecciona un producto...]" });
                list = list.OrderBy(p => p.Description).ToList();
                ViewBag.ProductID = new SelectList(list, "ProductID", "Description");
                ViewBag.Error = "Debe seleccionar un producto";
                return View(productOrder);

            }
           
            var product = db.Products.Find(productID);

            if (product == null)
            {
                var list = db.Products.ToList();
                list.Add(new Product { ProductID = 0, Description = "[Selecciona un producto...]" });
                list = list.OrderBy(p => p.Description).ToList();
                ViewBag.ProductID = new SelectList(list, "ProductID", "Description");
                ViewBag.Error = "El producto no existe";
                return View(productOrder);
            }

            productOrder = orderView.Products.FirstOrDefault(p => p.ProductID == productID);
            if (productOrder == null)
            {
                productOrder = new ProductOrder
                {
                    Description = product.Description,
                    Price       = product.Price,
                    ProductID   = product.ProductID,
                    Quantity    = float.Parse(Request["Quantity"])                
                };

                //Se agrega a la colección de productos de la ordenView, 
                //la orden productOrder como datos en memoria.
                orderView.Products.Add(productOrder);
            }
            else
            {
                productOrder.Quantity += float.Parse(Request["Quantity"]);
            }

            //Obtenemos en memoria los datos de la orden
            var listC = db.Customers.ToList();
            listC.Add(new Customer { CustomerID = 0, FirstName = "[Selecciona un cliente...]" });
            listC = listC.OrderBy(c => c.FullName).ToList();
            ViewBag.CustomerID = new SelectList(listC, "CustomerID", "FullName");

            return View("NewOrder", orderView);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}