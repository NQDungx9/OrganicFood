using OrganicFoodMVC.DataAccess.Data;
using OrganicFoodMVC.DataAccess.Repository.IRepository;
using OrganicFoodMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrganicFoodMVC.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            var objFromDb = _db.Products.FirstOrDefault(s => s.Id == product.Id);
            if(objFromDb != null)
            {
                if(product.ImageUrl != null)
                {
                    objFromDb.ImageUrl = product.ImageUrl;
                }

                objFromDb.Name = product.Name;
                objFromDb.Discription = product.Discription;
                objFromDb.Quantity = product.Quantity;
                objFromDb.Price = product.Price;
                objFromDb.CategoryId = product.CategoryId;
                objFromDb.BrandId = product.BrandId;
            }
        }
    }
}
