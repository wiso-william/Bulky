using Bulky.DataAccess;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAccess.Repository;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
        public IActionResult Index()
        {
            List<CategoryModel> objCategoryList = _unitOfWork.Category.GetAll().ToList(); 
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
                _unitOfWork.Category.Add(ele); // Tells what to add, makes it so you can do multiple adds before calling the db
                _unitOfWork.Save(); //Saves the changes to the db
                TempData["Success"] = "Category created successfully";
            return RedirectToAction("Index"); //Since we are in the same controller this is enough
            // if you want to go to a different controller ("Index","ControllerName")
            }
            return View(); 
        }

        public IActionResult Edit(int? id) {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            CategoryModel CategoryFromDb = _unitOfWork.Category.Get(u=>u.Id == id);
            if (CategoryFromDb == null) { 
            return NotFound();
            }
            return View(CategoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(CategoryModel ele) 
        {
            if (ModelState.IsValid) 
            {
                _unitOfWork.Category.Add(ele);
                _unitOfWork.Save();
                TempData["Success"] = "Category updated successfully";
                return RedirectToAction("Index"); 
                                                  
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            CategoryModel CategoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            if (CategoryFromDb == null)
            {
                return NotFound();
            }
            return View(CategoryFromDb);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            CategoryModel ele = _unitOfWork.Category.Get(u => u.Id == id);
            if (ele == null) return NotFound();
            _unitOfWork.Category.Remove(ele);
            _unitOfWork.Save();
            TempData["Success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
