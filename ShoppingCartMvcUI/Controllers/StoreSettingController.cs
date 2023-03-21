using ShoppingCartMvcUI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingCartMvcUI.Data;
using ShoppingCartMvcUI.Models;


namespace ShoppingCartMvcUI.Controllers
{
    public class StoreSettingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StoreSettingController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET all books
        public async Task<IActionResult> Index()
        {
            return _context.Books != null ?
                View(await _context.Books.ToListAsync()) :
                Problem("Entity set Books is null");
        }

        // GET: Books/Create

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        //Version 1.0

        //public ActionResult Create(Book book) 
        //{ 
        //    try
        //    {           
        //        if (ModelState.IsValid) 
        //        {
        //            using (ApplicationDbContext db = new ApplicationDbContext())
        //            {
        //                var newBook = new Book();
        //                newBook.Id = book.Id;
        //                newBook.AuthorName = book.AuthorName;
        //                newBook.Price = book.Price;
        //                newBook.GenreId= book.GenreId;

        //                db.Books.Add(newBook);
        //                db.SaveChanges();
        //            }
        //            return Redirect("Index");
        //        }

        //        return View(book);

        //    }
        //    catch (Exception ex) 
        //    {
        //        throw new Exception(ex.Message);
        //    }

        //}


        //version 2.0
         public async Task<IActionResult> Create([Bind("Id,BookName,AuthorName,Price,Image,GenreId")] Book book)
        {
            if (ModelState.IsValid == false)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "GenreName", book.GenreId);
            return View(book);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "GenreName", book.GenreId);
            return View(book);
        }

        // POST: SettingsBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookName,AuthorName,Price,Image,GenreId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid == false)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "GenreName", book.GenreId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'BookStore'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }


}
