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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
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
            //IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().ToList().Select(u => new SelectListItem
            //{
            //    Text = u.Name,
            //    Value = u.CategoryId.ToString()
            //});
            ////ViewBag.CategoryList = CategoryList;
            ////ViewData["CategoryList"] = CategoryList;
            //if (id != 0 && id != null)
            //{
            //    ProductVM productVM = new ProductVM
            //    {
            //        Product = _unitOfWork.Product.Get(u => u.ProductId == id),

            //        CategoryList = CategoryList
            //    };
            //    return View(productVM);
            //}
            //else
            //{
            //    ProductVM productVM = new ProductVM
            //    {
            //        Product = new(),
            //        CategoryList = CategoryList
            //    };
            //    return View(productVM);
            //}
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString()
                }),
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _unitOfWork.Product.Get(u => u.ProductId == id);
                return View(productVM);
            }

        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.Product.ImageURL = @"images\product\" + fileName;
                }

                _unitOfWork.Product.Add(productVM.Product);
                //if (productVM.Product.ProductId == 0)
                //{
                //    _unitOfWork.Product.Add(productVM.Product);
                //}
                //else
                //{
                //    _unitOfWork.Product.Update(productVM.Product);
                //}
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
