using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskEFC.Models;

namespace TaskEFC.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ShoppingContext _context;
        public OrdersController(ShoppingContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Orders.Include(u => u.Supermarket).Include(u => u.Customer).ToList());
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = _context.Orders
                .FirstOrDefault(c => c.Id == id);
            
            _context.SaveChanges();
            
            return View(order);            
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                var editedOrder = _context.Orders.Where(a => a.Id == order.Id).FirstOrDefault();
                if (editedOrder != null)
                {
                    editedOrder.CustomerId = order.CustomerId;
                    editedOrder.SupermarketId = order.SupermarketId;
                    editedOrder.OrderDate = order.OrderDate;
                }
                _context.SaveChanges();
                return View("Details", order);
            }
            return View("Index");
        }       
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = _context.Orders.Include(u => u.OrderDetails).ThenInclude(u => u.Product)
                .FirstOrDefault(c => c.Id == id);
                
            return View(order);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var deletedOrder = _context.Orders.Where(c => c.Id == id).FirstOrDefault();
            if (deletedOrder != null)
            {
                _context.Orders.Remove(deletedOrder);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View(new Order { });
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(order);

                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException)
            {               
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(order);
        }
    }
}