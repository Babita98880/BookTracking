using Microsoft.AspNetCore.Mvc;
using Web1001_BookTracking.Data;
using Web1001_BookTracking.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
namespace Web1001_BookTracking.Controllers
{
    public class CategoryTypesController : Controller
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly BookContext _db;

        public CategoryTypesController(BookContext context, ILogger<CategoriesController> logger)
        {
            _db = context;
            _logger = logger;
        }
        // get category list with category type
        public async Task<IActionResult> Index()
        {
              var categoryTypes = await _db.CategoryTypes
                .ToListAsync();
                return View(categoryTypes);
            
        }

        public IActionResult Create()
        {
            return View();
        }
     

             // add new category
        [HttpPost]
        public async Task<IActionResult> Create(CategoryType categoryTypes)
        {

            if (ModelState.IsValid)
            {
                
                _db.CategoryTypes.Add(categoryTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(categoryTypes);
        }

    // Delete a category
        public async Task<IActionResult> Delete(int id)
        {
            var categoryTypes = await _db.CategoryTypes.FindAsync(id);
            if (categoryTypes == null)
            {
                return NotFound();
            }

            _db.CategoryTypes.Remove(categoryTypes);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
