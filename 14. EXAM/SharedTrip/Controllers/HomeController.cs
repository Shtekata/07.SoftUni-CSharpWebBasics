namespace SharedTrip.App.Controllers
{
    using SharedTrip.Services;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class HomeController : Controller
    {
        private readonly ITripsService tripService;

        public HomeController(ITripsService tripService)
        {
            this.tripService = tripService;
        }
        public HttpResponse Index()
        {
            if (IsUserLoggedIn())
            {
                return Redirect("/Trips/All");
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
