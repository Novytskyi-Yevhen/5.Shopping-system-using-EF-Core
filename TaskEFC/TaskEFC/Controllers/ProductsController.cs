using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskEFC.Models;

namespace TaskEFC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ShoppingContext _context;
        public ProductsController(ShoppingContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View( _context.Products.OrderBy(a=>a.Name).ToList());
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _context.Products
                .FirstOrDefault(c => c.Id == id);
            
            _context.SaveChanges();
            
            return View(product);            
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var editedProduct = _context.Products.Where(a => a.Id == product.Id).FirstOrDefault();
                if (editedProduct != null)
                {
                    editedProduct.Name = product.Name;
                    editedProduct.Price = product.Price;
                }
                _context.SaveChanges();
                return View("Details", product);
            }
            return View("Index");
        }       
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _context.Products
                .FirstOrDefault(c => c.Id == id);
                
            return View(product);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var deletedProduct = _context.Products.Where(c => c.Id == id).FirstOrDefault();
            if (deletedProduct != null)
            {
                _context.Products.Remove(deletedProduct);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View(new Product { });
        }


        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException)
            {               
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(product);
        }
    }
}