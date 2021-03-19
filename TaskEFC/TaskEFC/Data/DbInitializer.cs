using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskEFC.Models;

namespace TaskEFC.Data
{
    public class DbInitializer
    {
        public static void Initialize(ShoppingContext context)
        {
            context.Database.EnsureCreated();
            if (context.Products.Any())
            {
                return;
            }
            var products = new Product[]
            {
                new Product{ Name = "Banana", Price = 26.50F},
                new Product{ Name = "Butter", Price = 78.00F},
                new Product{ Name = "CocaCola", Price = 19.60F},
                new Product{ Name = "Milk", Price = 23.20F},
                new Product{ Name = "IceCream", Price = 89.50F },
                new Product{ Name = "Snickers", Price = 19.50F },
                new Product{ Name = "Bread", Price = 15.00F },
                new Product{ Name = "PopCorn", Price = 5.00F },
                new Product{ Name = "Potatoes", Price = 18.00F }
            };
            foreach (Product p in products)
            {
                context.Products.Add(p);
            }
            context.SaveChanges();

            var customers = new Customer[]
            {
                new Customer{FirstName = "Zosya", LastName = "Sinitsina", Address = "Rivne", Discount = 0},
                new Customer{FirstName = "Semiuel", LastName = "Panikovskyi", Address = "Vinnytsya", Discount = 5},
                new Customer{FirstName = "Adam", LastName = "Vertman", Address = "Odesa", Discount = 15},
                new Customer{FirstName = "Ostap", LastName = "Lugovyi", Address = "Zhmerynka", Discount = 0},
                new Customer{FirstName = "Katya", LastName = "Osadcha", Address = "Kyiv", Discount = 20},
                new Customer{FirstName = "Vadym", LastName = "Grachov", Address = "Dnipro", Discount = 5},
                new Customer{FirstName = "Olya", LastName = "Ivanchenko", Address = "Sumy", Discount = 5}
            };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();

            var supermarkets = new Supermarket[]
            {
                new Supermarket{Name = "Wellmart", Address = "Odesa"},
                new Supermarket{Name = "Big Pocket", Address = "Rivne"},
                new Supermarket{Name = "Arsen", Address = "Zhmerynka"},
                new Supermarket{Name = "Ashan", Address = "Lviv"},
                new Supermarket{Name = "Veles 555", Address = "Dnipro"},
                new Supermarket{Name = "Metro", Address = "Sumy"},
                new Supermarket{Name = "ATB", Address = "Kyiv"},
                new Supermarket{Name = "Silpo", Address = "Vinnytsya"}                
            };
            foreach (Supermarket s in supermarkets)
            {
                context.Supermarkets.Add(s);
            }
            context.SaveChanges();

            var orders = new Order[]
            {
                new Order{SupermarketId = 1,  CustomerId = 1, OrderDate = DateTime.Now},
                new Order{SupermarketId = 2,  CustomerId = 4, OrderDate = DateTime.Now},
                new Order{SupermarketId = 1,  CustomerId = 5, OrderDate = DateTime.Now},
                new Order{SupermarketId = 3,  CustomerId = 1, OrderDate = DateTime.Now},
                new Order{SupermarketId = 6,  CustomerId = 3, OrderDate = DateTime.Now},
                new Order{SupermarketId = 1,  CustomerId = 2, OrderDate = DateTime.Now},
                new Order{SupermarketId = 4,  CustomerId = 1, OrderDate = DateTime.Now}
            };
            foreach (Order s in orders)
            {
                context.Orders.Add(s);
            }
            context.SaveChanges();
        }
    }
}
