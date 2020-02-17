namespace Andreys.App.Controllers
{
    using Andreys.Services;
    using Andreys.ViewModels.Products;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class HomeController : Controller
    {
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService)
        {
            this.productsService = productsService;
        }
        public HttpResponse Index()
        {
            if (IsUserLoggedIn())
            {
                var products = productsService.GetAll(x => new ProductInfoViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    ImageUrl = x.ImageUrl
                });

                var viewModel = new AllProductsViewModel
                {
                    Products = products,
                };
                return View(viewModel, "Home");
            }
            return this.View();
        }

        [HttpGet("/")]
        public HttpResponse IndexFullPath()
        {
            return Index();
        }
    }
}
