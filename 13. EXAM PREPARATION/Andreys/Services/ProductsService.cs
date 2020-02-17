using Andreys.Data;
using Andreys.Models;
using Andreys.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Andreys.Services
{
    public class ProductsService : IProductsService
    {
        private readonly AndreysDbContext db;

        public ProductsService(AndreysDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<T> GetAll<T>(Func<Product, T> selectFunc)
        {
            var allProducts = db.Products.Select(selectFunc).ToList();
            return allProducts;
        }

        public void Add(AddProductInputModel input)
        {
            var product = new Product
            {
                Name = input.Name,
                Description = input.Description,
                ImageUrl = input.ImageUrl,
                Price = input.Price,
                Category = Enum.Parse<Category>(input.Category),
                Gender = Enum.Parse<Gender>(input.Gender),
            };

            db.Products.Add(product);
            db.SaveChanges();
        }

        public ProductDetailsDataModel GetDetails(int id)
        {
            var product = db.Products.Where(x => x.Id == id)
                .Select(x => new ProductDetailsDataModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    Category = x.Category.ToString(),
                    Gender = x.Gender.ToString(),
                }).FirstOrDefault();
            return product;

        }

        public void Delete(int id)
        {
            var product = db.Products.Find(id);
            db.Remove(product);
            db.SaveChanges();
        }
    }
}

