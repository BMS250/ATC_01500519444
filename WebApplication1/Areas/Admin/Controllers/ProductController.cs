using BookWeb.DataAccess.Repository.IRepository;
using BookWeb.Models;
using BookWeb.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

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
            // My Idea
            List<Category> catergories = _unitOfWork.Category.GetAll().ToList();
            Tuple<List<Product>, List<Category>> tuple = new(products, catergories);
            return View(tuple);
        }
        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.CategoryId.ToString()
            });
            //ViewBag.CategoryList = CategoryList;
            //ViewData["CategoryList"] = CategoryList;
            ProductVM productVM;
            if (id != 0 && id != null)
            {
                productVM = new ProductVM
                {
                    Product = _unitOfWork.Product.Get(u => u.ProductId == id),
                    
                    CategoryList = CategoryList
                };
            }
            else
            {
                productVM = new ProductVM
                {
                    Product = new(),
                    CategoryList = CategoryList
                };
            }
            return View(productVM);
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = $"{productVM.Product.Title} has been upserted successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = $"Failed to add this Product";
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
