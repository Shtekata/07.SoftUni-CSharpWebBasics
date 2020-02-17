using Andreys.Models;
using Andreys.ViewModels.Products;
using System;
using System.Collections.Generic;

namespace Andreys.Services
{
    public interface IProductsService
    {
        IEnumerable<T> GetAll<T>(Func<Product, T> selectFunc);

        ProductDetailsDataModel GetDetails(int id);

        public void Add(AddProductInputModel input);

        public void Delete(int id);
    }
}
