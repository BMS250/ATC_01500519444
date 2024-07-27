using BookWeb.DataAccess.Data;
using BookWeb.DataAccess.Repository.IRepository;
using BookWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWeb.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            //_db.Products.Update(product);
            // Not neccessary
            var objFromDb = _db.Products.FirstOrDefault(u => u.ProductId == product.ProductId);
            if (objFromDb != null)
            {
                objFromDb.Title = product.Title;
                objFromDb.Description = product.Description;
                objFromDb.CId = product.CId;
                objFromDb.ISBN = product.ISBN;
                objFromDb.PriceList = product.PriceList;
                objFromDb.Price = product.Price;
                objFromDb.Price50 = product.Price50;
                objFromDb.Price100 = product.Price100;
                objFromDb.Author = product.Author;
                objFromDb.ProductImages = product.ProductImages;
                //if (product.ImageURL != null)
                //{
                //    objFromDb.ImageURL = product.ImageURL;
                //}
            }
        }
    }
}
