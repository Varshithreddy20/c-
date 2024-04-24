using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shopping;

namespace LINQ_assignment
{
   
    public class Program
    {
        static void Main(string[] args)
        {
            List<Customer> customer = new List<Customer> {

                new Customer { CustomerId = 1, CustomerName = "John Doe", Email = "john@example.com" ,Location = "USA"},
                new Customer { CustomerId = 2, CustomerName = "Jane Smith", Email = "jane@example.com",Location = "Uk" },
                new Customer { CustomerId = 3, CustomerName = "Alice Johnson", Email = "alice@example.com",Location = "INDIA" },
                new Customer { CustomerId = 4, CustomerName = "Bob Brown", Email = "bob@example.com" , Location = "CHINA"},
                new Customer { CustomerId = 5, CustomerName = "Emma Davis", Email = "emma@example.com" , Location = "USA"}
                };
            List<product> products = new List<product>
            {
                new product { ProductId = 1,ProductName=" mobile", Price= 35000},
                new product { ProductId = 2,ProductName=" laptop", Price = 80000 },
                new product { ProductId = 1, ProductName = "mobile", Price = 35000 },
                new product { ProductId = 2, ProductName = "laptop", Price = 80000 },
                new product { ProductId = 3, ProductName = "tablet", Price = 25000 },
                new product { ProductId = 4, ProductName = "smartwatch", Price = 5000 },
                new product { ProductId = 5, ProductName = "headphones", Price = 3000 },
                new product { ProductId = 6, ProductName = "camera", Price = 45000 },
                new product { ProductId = 7, ProductName = "television", Price = 60000 },
                new product { ProductId = 8, ProductName = "gaming console", Price = 35000 },
                new product { ProductId = 9, ProductName = "speaker", Price = 8000 },
                new product { ProductId = 10, ProductName = "external hard drive", Price = 5000 }
            };
            List<Order> orders = new List<Order>() 
            {
                new Order { OrderId = 1, OrderDate = new DateTime()},
                new Order { OrderId = 2, OrderDate = new DateTime()},
                new Order { OrderId = 3, OrderDate = new DateTime()},
                new Order { OrderId = 4, OrderDate = new DateTime()},
                new Order { OrderId = 5, OrderDate = new DateTime()},
            };

            List<OrderDetail> ordersDetail = new List<OrderDetail>() 
            {
                new OrderDetail { OrderId = 1,CustomerId = 2, ProductName = " mobile", OrderDispatched = " yes",Location="USA"},
                new OrderDetail { OrderId = 1, CustomerId = 5, ProductName = "mobile", OrderDispatched = "no" ,Location="UK"},
                new OrderDetail { OrderId = 2, CustomerId = 8, ProductName = "laptop", OrderDispatched = "no" ,Location="UK"},
                new OrderDetail { OrderId = 3, CustomerId = 3, ProductName = "tablet", OrderDispatched =  " yes", Location = "INDIA"},
                new OrderDetail { OrderId = 4, CustomerId = 6, ProductName = "smartwatch", OrderDispatched =  " yes", Location = "USA"},
                new OrderDetail { OrderId = 5, CustomerId = 7, ProductName = "headphones", OrderDispatched =  " yes", Location = "USA"},
                new OrderDetail { OrderId = 6, CustomerId = 9, ProductName = "camera", OrderDispatched = "no" , Location = "CHINA"},
                new OrderDetail { OrderId = 7, CustomerId = 4, ProductName = "television", OrderDispatched ="no" , Location = "CHINA"},
                new OrderDetail { OrderId = 8, CustomerId = 1, ProductName = "gaming console", OrderDispatched = " yes", Location = "INDIA"},
                new OrderDetail { OrderId = 9, CustomerId = 10, ProductName = "speaker", OrderDispatched =  " yes", Location = "INDIA"},
                new OrderDetail { OrderId = 10, CustomerId = 2, ProductName = "external hard drive", OrderDispatched = "no" , Location = "USA"},

            };
            var sortedCustomers = customer.OrderBy(Customer => Customer.CustomerName).ToList();
            Console.WriteLine("List of all customers in alphabetical order by name ");
            Console.WriteLine();

            foreach (Customer item in sortedCustomers)
            {
                Console.WriteLine($"Id: {item.CustomerId}, Name: {item.CustomerName}, Email: {item.Email}");
            }

            Console.WriteLine();    
            var productPrice = products.OrderBy(Product => Product.Price).ToList();
            Console.WriteLine("List of all products in order of unit price, from highest to lowest ");
            Console.WriteLine();

            foreach (product item in productPrice)
            {
                Console.WriteLine($"Id: {item.ProductId}, Name: {item.ProductName}, Email: {item.Price}");
            }
            Console.WriteLine();
       
            var custlocation = ordersDetail.Where(OrderDetail => OrderDetail.Location.Contains("USA")).OrderBy(OrderDetail => OrderDetail.Location).ToList();

            Console.WriteLine(" list of all orders that were shipped to customers in the USA");
            Console.WriteLine();

            foreach (OrderDetail item in custlocation)
            {
                Console.WriteLine($"Id: {item.OrderId}, Name: {item.CustomerId}, Product Name: {item.ProductName}, Location: {item.Location}, Order Dispatched: {item.OrderDispatched}");
            }
        }
    }
}
