namespace DealerShip.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DealerShip.Services.Data;
    using DealerShip.Web.ViewModels.Cars.InputModels;
    using DealerShip.Web.ViewModels.Cars.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class CarsController : BaseController
    {
        private readonly ICarService carService;

        public CarsController(ICarService carService)
        {
            this.carService = carService;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateCarInputModel inputModel)
        {
            await this.carService.AddAsync(inputModel.Make, inputModel.Model, inputModel.Price);

            return this.Redirect("/");
        }

        public async Task<IActionResult> All()
        {
            var allCars = await this.carService.GetAllCars<CarViewModel>();

            return this.View(allCars);
        }
    }
}
