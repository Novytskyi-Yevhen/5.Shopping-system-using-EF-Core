using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TaskEFC.Models;

namespace TaskEFC.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ShoppingContext _context;
        public CustomersController(ShoppingContext context)
        {
            _context = context;
        }
        public IActionResult Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AddressSortParm"] = String.IsNullOrEmpty(sortOrder) ? "addr_desc" : "address";
            ViewData["CurrentFilter"] = searchString;
            var _customersList = from list in _context.Customers
                                 select list;
            if (!String.IsNullOrEmpty(searchString))
            {
                _customersList = _customersList.Where(s => s.LastName.Contains(searchString)
                               || s.FirstName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    _customersList = _customersList.OrderByDescending(c => c.LastName);
                    break;
                case "addr_desc":
                    _customersList = _customersList.OrderByDescending(c => c.Address);
                    break;
                case "address":
                    _customersList = _customersList.OrderBy(c => c.Address);
                    break;
                default:
                    _customersList = _customersList.OrderBy(c => c.LastName);
                    break;
            }
            return View(_customersList.AsNoTracking().ToList());
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = _context.Customers
                .FirstOrDefault(c => c.Id == id);
            
            _context.SaveChanges();
            
            return View(customer);            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Customer customer)
        {               
            if (ModelState.IsValid)
            {
                var editedCustomer = _context.Customers.Where(a => a.Id == customer.Id).FirstOrDefault();
                if (editedCustomer != null)
                {
                    editedCustomer.FirstName = customer.FirstName;
                    editedCustomer.LastName = customer.LastName;
                    editedCustomer.Address = customer.Address;
                    editedCustomer.Discount = customer.Discount;
                }
                _context.SaveChanges();
                return View("Details", customer);
            }
            return View("Index");            
        }       
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = _context.Customers
                .FirstOrDefault(c => c.Id == id);
                
            return View(customer);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var deletedCustomer = _context.Customers.Where(c => c.Id == id).FirstOrDefault();
            if (deletedCustomer != null)
            {
                _context.Customers.Remove(deletedCustomer);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View(new Customer { });
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(customer);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException)
            {               
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "contact your system administrator.");
            }
            return View(customer);
        }
    }
}