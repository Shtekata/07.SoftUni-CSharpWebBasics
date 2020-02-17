using IRunes.App.ViewModels.Tracks;
using IRunes.Services;
using SIS.HTTP;
using SIS.MvcFramework;

namespace IRunes.App.Controllers
{
    public class TracksController : Controller
    {
        private readonly ITracksService tracksService;

        public TracksController(ITracksService tracksService)
        {
            this.tracksService = tracksService;
        }
        public HttpResponse Create(string albumId)
        {
            if (!IsUserLoggedIn())
            {
                return Redirect("/Users/Login");
            }

            var viewModel = new CreateViewModel();
            viewModel.AlbumId = albumId;

            return View(viewModel);
        }


        [HttpPost]
        public HttpResponse Create(CreateInputModel input)
        {
            if (!IsUserLoggedIn())
            {
                return Redirect("/Users/Login");
            }

            if (input.Name.Length < 4 || input.Name.Length > 20)
            {
                return Error("Track name should be between 4 and 20 characters.");
            }

            if (!input.Link.StartsWith("http"))
            {
                return Error("Invalid link.");
            }

            if (input.Price < 0)
            {
                return Error("Price should be a positive number.");
            }

            tracksService.Create(input.AlbumId, input.Name, input.Link, input.Price);

            return Redirect("/Albums/Details?id=" + input.AlbumId);
        }

        public HttpResponse Details(string albumId, string trackId)
        {
            var trackDetails = tracksService.GetTrack(trackId);

            if (trackDetails == null)
            {
                return Error("Track not found.");
            }

            var viewModel = new DetailsViewModel
            {
                AlbumId = albumId,
                Name = trackDetails.Name,
                Link = trackDetails.Link,
                Price = trackDetails.Price,
            };

            return View(viewModel);
        }
    }
}
