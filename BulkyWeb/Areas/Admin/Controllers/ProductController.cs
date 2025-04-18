using Bulky.DataAccess;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAccess.Repository;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList(); 
            return View(objProductList); // What is passed to the view when the Index action is called
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] // Annotation that allows http comunication
        public IActionResult Create(Product ele) //Same type as the one passed to the view, so to the post form
        {
            if (ModelState.IsValid) //Se passa i validator
            {
                _unitOfWork.Product.Add(ele); // Tells what to add, makes it so you can do multiple adds before calling the db
                _unitOfWork.Save(); //Saves the changes to the db
                TempData["Success"] = "Product created successfully";
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
            Product? ProductFromDb = _unitOfWork.Product.Get(u=>u.Id == id);
            if (ProductFromDb == null) { 
            return NotFound();
            }
            return View(ProductFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Product ele) 
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is not valid");
                return View(ele);
            }
            if (ModelState.IsValid) 
            {
                _unitOfWork.Product.Update(ele);
                Console.WriteLine("sono nell save");
                _unitOfWork.Save();
                TempData["Success"] = "Product updated successfully";
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
            Product ProductFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            if (ProductFromDb == null)
            {
                return NotFound();
            }
            return View(ProductFromDb);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product ele = _unitOfWork.Product.Get(u => u.Id == id);
            if (ele == null) return NotFound();
            _unitOfWork.Product.Remove(ele);
            _unitOfWork.Save();
            TempData["Success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
