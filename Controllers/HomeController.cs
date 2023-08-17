using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web1001_BookTracking.Models;
using Web1001_BookTracking.Data;
using Microsoft.EntityFrameworkCore;

namespace Web1001_BookTracking.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BookContext _db;
    public HomeController(BookContext context,ILogger<HomeController> logger)
    {
        _logger = logger;
         _db = context;
    }

    public async Task<IActionResult> Index()
    {
         var books = await _db.Books
                .Include(c => c.Category) // Include Category navigation property
                .ToListAsync();
                return View(books);
    }
     public IActionResult Create()
        {
            ViewBag.Category = _db.Categories.ToList();
            LoggerExtensions.LogInformation(_logger, "CategoryTypes count: {Count}", ViewBag.Category.Count);
            return View();
        }

          // add new category
        [HttpPost]
     public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                // Check if the selected TypeId is valid
                var validCategoryType = await _db.Categories.FindAsync(book.CategoryId);
                if (validCategoryType == null)
                {
                    ModelState.AddModelError("TypeId", "Invalid Category selected.");
                    return View(book);
                }
                _db.Books.Add(book);
                await _db.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
