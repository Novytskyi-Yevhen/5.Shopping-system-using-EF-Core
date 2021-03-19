using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskEFC.Models;

namespace TaskEFC.Controllers
{
    public class SupermarketsController : Controller
    {
        private readonly ShoppingContext _context;
        public SupermarketsController(ShoppingContext context)
        {
            _context = context;
        }
        //public IActionResult Index()
        //{
        //    return View(_context.Supermarkets.ToList());
        //}

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 3;   // количество элементов на странице

            IQueryable<Supermarket> source = _context.Supermarkets;
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                supermarkets = items
            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var supermarket = _context.Supermarkets
                .FirstOrDefault(c => c.Id == id);
            
            _context.SaveChanges();
            
            return View(supermarket);            
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Supermarket supermarket)
        {
            if (ModelState.IsValid)
            {
                var editedSypermarket = _context.Supermarkets.Where(a => a.Id == supermarket.Id).FirstOrDefault();
                if (editedSypermarket != null)
                {
                    editedSypermarket.Name = supermarket.Name;
                    editedSypermarket.Address = supermarket.Address;
                }
                _context.SaveChanges();
                return View("Details", supermarket);
            }
            return View("Index");
        }       
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var supermarket = _context.Supermarkets
                .FirstOrDefault(c => c.Id == id);
                
            return View(supermarket);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var deletedSupermarket = _context.Supermarkets.Where(c => c.Id == id).FirstOrDefault();
            if (deletedSupermarket != null)
            {
                _context.Supermarkets.Remove(deletedSupermarket);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View(new Supermarket { });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Supermarket supermarket)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(supermarket);
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
            return View(supermarket);
        }
    }
}