using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
            
        }
        public IActionResult Index()
        {
            List<CategoryModel> objCategoryList = _db.Categories.ToList(); 
            return View(objCategoryList); // What is passed to the view when the Index action is called
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] // Annotation that allows http comunication
        public IActionResult Create(CategoryModel ele) //Same type as the one passed to the view, so to the post form
        {
            if (ModelState.IsValid) //Se passa i validator
            {
                _db.Categories.Add(ele); // Tells what to add, makes it so you can do multiple adds before calling the db
                _db.SaveChanges(); //Saves the changes to the db
            return RedirectToAction("Index"); //Since we are in the same controller this is enough
            // if you want to go to a different controller ("Index","ControllerName")
            }
            return View(); 
        }
    }
}
