using BookWeb.DataAccess.Repository.IRepository;
using BookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace MyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //private readonly ApplicationDbContext _db;
        //public CategoryController(ApplicationDbContext db)
        //{
        //    _db = db;
        //}

        public IActionResult Index()
        {
            
            List<Category> objCategoriesList = _unitOfWork.Category.GetAll().ToList();
            //List<Category> objCategoriesList = _db.Categories.ToList();
            return View(objCategoriesList);
        }
        public IActionResult Create()
        {
            // No need for it, it is created automatically
            //Category objCategory = new Category();
            //return View(objCategory);
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == "kkk")
            {
                ModelState.AddModelError("name", "You should write a valid name");
            }
            //if (obj.DisplayOrder <= 0)
            //{
            //    ModelState.AddModelError("displayorder", "You should write a valid order");
            //}
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                //_db.Categories.Add(obj);
                //_db.SaveChanges();
                TempData["success"] = $"{obj.Name} has been created successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = $"Failed to create this category";
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = $"The Category ID can't be {id}";
                return NotFound();
            }
            Category? obj = _unitOfWork.Category.Get(u => u.CategoryId == id);
            //Category? obj = _db.Categories.FirstOrDefault(u => u.CategoryId == id);
            if (obj == null)
            {
                TempData["error"] = $"The Category is not found";
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                //_db.Categories.Update(obj);
                //_db.SaveChanges();
                TempData["success"] = $"{obj.Name} has been edited successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        // You can delete just using this method without adding a new view 
        /*public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound(); 
            }
            Category? category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound(); 
            }
            _db.Categories.Remove(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }*/
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = $"The Category ID can't be {id}";
                return NotFound();
            }
            Category? obj = _unitOfWork.Category.Get(u => u.CategoryId == id);
            //Category? obj = _db.Categories.FirstOrDefault(u => u.CategoryId == id);
            if (obj == null)
            {
                TempData["error"] = $"The Category is not found";
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Delete(Category obj)
        {
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            //_db.Categories.Remove(obj);
            //_db.SaveChanges();
            TempData["success"] = $"{obj.Name} has been deleted successfully";
            return RedirectToAction("Index");
        }
        //OR
        /*[HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? category = _db.Categories.Find(id);
            _db.Categories.Remove(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }*/
    }
}
