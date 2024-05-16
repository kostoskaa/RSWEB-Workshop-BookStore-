using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.ProjectModel;
using WebApplication3.Data;
using WebApplication3.Models;
using WebApplication3.ViewModels;


namespace WebApplication3.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index( string searchString)
        {
            //var applicationDbContext = _context.Book.Include(b => b.Author).Include(m => m.Genres).ThenInclude(m => m.Genre);
            //return View(await applicationDbContext.ToListAsync());
            IQueryable<Book> books = _context.Book.Include(m => m.Author).Include(m => m.Genres).ThenInclude(m => m.Genre).Include(m => m.Reviews);
            IQueryable<string> title = _context.Book.OrderBy(x => x.Title).Select(x => x.Title).Distinct();
            if (!string.IsNullOrEmpty(searchString))
            {
                books=books.Where(s=>s.Title.Contains(searchString)||s.Author.FirstName.Contains(searchString)||s.Author.LastName.Contains(searchString)||s.Genres.Any(s=>s.Genre.GenreName.Contains(searchString)));
            }
            foreach (var book in books)
            {
                var distinctGenres = book.Genres
                    .Where(b => b.Genre != null)
                    .GroupBy(b => b.GenreId)
                    .Select(b => b.First())
                    .ToList();
                book.Genres = distinctGenres;


            }

            var bookAuthorVM = new BookAuthorGenreViewModel
            {
                Genres = new SelectList(await title.ToListAsync()),
                Books = await books.ToListAsync()
            };
            return View(bookAuthorVM);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Author)
                .Include(m => m.Genres).ThenInclude(m => m.Genre)
                .Include(m => m.Reviews)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            var averageRating = book.AverageRating();
            averageRating = Double.Round(averageRating, 2);
            ViewData["AverageRating"] = averageRating;
            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "FullName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,YearPublished,NumPages,Description,Publisher,FrontPage,DownloadUrl,AuthorId")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "FullName", book.AuthorId);
            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _context.Book.Where(b => b.Id == id).Include(b => b.Genres).First();
            if (book == null)
            {
                return NotFound();
            }
            var genres = _context.Genre.AsEnumerable();
            genres = genres.OrderBy(s => s.GenreName);

            BookGenresEditViewModel viewmodel = new BookGenresEditViewModel
            {
                Book = book,
                GenreList = new MultiSelectList(genres, "Id", "GenreName"),
                SelectedGenres = book.Genres.Select(sa => sa.GenreId)
            };

            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "FullName", book.AuthorId);
            return View(viewmodel);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, BookGenresEditViewModel viewmodel)
        {
            if (id != viewmodel.Book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewmodel.Book);
                    IEnumerable<int> newGenreList = viewmodel.SelectedGenres;
                    IEnumerable<int> prevGenreList = _context.BookGenre.Where(s => s.BookId == id).Select(s => s.GenreId);
                    IQueryable<BookGenre> toBeRemoved = _context.BookGenre.Where(s=>s.BookId == id);
                    if (newGenreList != null)
                    {
                        toBeRemoved = toBeRemoved.Where(s=>!newGenreList.Contains(s.GenreId));
                        foreach(int genreId in newGenreList)
                        {
                            if (!prevGenreList.Any(s => s == genreId))
                            {

                                _context.BookGenre.Add(new BookGenre {  GenreId = genreId, BookId = id });
                            }
                        }
                    }
                    _context.BookGenre.RemoveRange(toBeRemoved);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(viewmodel.Book.Id))
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
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "FullName", viewmodel.Book.AuthorId);
            return View(viewmodel);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Author)
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
