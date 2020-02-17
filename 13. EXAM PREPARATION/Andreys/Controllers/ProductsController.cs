using Andreys.Services;
using Andreys.ViewModels.Products;
using SIS.HTTP;
using SIS.MvcFramework;

namespace Andreys.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public HttpResponse Add()
        {
            if (!IsUserLoggedIn())
            {
                return Redirect("/Users/Login");
            }

            return View();
        }


        [HttpPost]
        public HttpResponse Add(AddProductInputModel input)
        {
            if (!IsUserLoggedIn())
            {
                return Redirect("/Users/Login");
            }

            if (input.Name.Length < 4 || input.Name.Length > 20)
            {
                return Redirect("/Products/Add");
            }

            if (input.Description.Length > 10)
            {
                return Redirect("/Products/Add");
            }

            if (!input.ImageUrl.StartsWith("http"))
            {
                return Redirect("/Products/Add");
            }

            if (input.Price < 0)
            {
                return Redirect("/Products/Add");
            }

            productsService.Add(input);
            
            return Redirect("/");
        }

        public HttpResponse Details(int id)
        {
            if (!IsUserLoggedIn())
            {
                return Redirect("/Users/Login");
            }

            var product = productsService.GetDetails(id);

            var viewModel = new ProductDetailsViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Category = product.Category,
                Gender = product.Gender,
            };

            return View(viewModel);
        }

        public HttpResponse Delete(int id)
        {
            productsService.Delete(id);

            return Redirect("/");
        }
    }
}

