using BookWeb.DataAccess.Repository.IRepository;
using BookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace MyBookWeb.Areas.Admin.Controllers
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
            List<Product> products = _unitOfWork.Product.GetAll().ToList();
            return View(products);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(product);
                _unitOfWork.Save();
                TempData["success"] = $"{product.Title} has been created successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = $"Failed to add this Product";
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                TempData["error"] = $"The Product ID can't be {id}";
                return View();
            }
            Product? product = _unitOfWork.Product.Get(u => u.ProductId == id);
            if (product == null)
            {
                TempData["error"] = $"The Product is not found";
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(product);
                _unitOfWork.Save();
                TempData["success"] = $"{product.Title} has been edited successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                TempData["error"] = $"The Product ID can't be {id}";
                return NotFound();
            }
            Product? product = _unitOfWork.Product.Get(u => u.ProductId == id);
            if (product == null)
            {
                TempData["error"] = $"The Product is not found";
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        public IActionResult Delete(Product product)
        {
            //if (ModelState.IsValid)
            //{
                _unitOfWork.Product.Remove(product);
                _unitOfWork.Save();
            TempData["success"] = $"{product.Title} has been deleted successfully";
            return RedirectToAction("Index");
            //}
            //return View();
        }
    }
}
