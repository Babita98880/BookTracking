using Microsoft.AspNetCore.Mvc;
using Web1001_BookTracking.Data;
using Web1001_BookTracking.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
namespace Web1001_BookTracking.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly BookContext _db;

        public CategoriesController(BookContext context, ILogger<CategoriesController> logger)
        {
            _db = context;
            _logger = logger;
        }
        // get category list with category type
        public async Task<IActionResult> Index()
        {
              var categories = await _db.Categories
                .Include(c => c.CategoryType) // Include CategoryType navigation property
                .ToListAsync();
                return View(categories);
            
        }

        public IActionResult Create()
        {
            ViewBag.CategoryTypes = _db.CategoryTypes.ToList();
            LoggerExtensions.LogInformation(_logger, "CategoryTypes count: {Count}", ViewBag.CategoryTypes.Count);
            return View();
        }

             // add new category
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {

            if (ModelState.IsValid)
            {
                // Check if the selected TypeId is valid
                var validCategoryType = await _db.CategoryTypes.FindAsync(category.TypeId);
                if (validCategoryType == null)
                {
                    ModelState.AddModelError("TypeId", "Invalid Category Type selected.");
                    return View(category);
                }
                _db.Categories.Add(category);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

    }
}
