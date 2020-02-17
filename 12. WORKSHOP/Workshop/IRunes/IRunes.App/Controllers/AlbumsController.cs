using IRunes.App.ViewModels.Albums;
using IRunes.Services;
using SIS.HTTP;
using SIS.MvcFramework;
using System.Collections.Generic;
using System.Linq;

namespace IRunes.App.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumsService albumsService;

        public AlbumsController(IAlbumsService albumsService)
        {
            this.albumsService = albumsService;
        }

        public HttpResponse All()
        {
            if (!IsUserLoggedIn())
            {
                return Redirect("/Users/Login");
            }

            //var albums = albumsService.GetAll()
            //    .Select(x => new AlbumInfoViewModel
            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //});

            var albums = albumsService.GetAllNew(x => new AlbumInfoViewModel
            {
                Id = x.Id,
                Name = x.Name
            });

            var viewModel = new AllAlbumsViewModel
            {
                Albums = albums,
            };

            return View(viewModel);
        }

        public HttpResponse Create()
        {
            return View();
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
                return Error("Name should be with length between 4 and 20.");
            }

            if (string.IsNullOrWhiteSpace(input.Cover))
            {
                return Error("Cover is required.");
            }

            albumsService.Create(input.Name, input.Cover);
            return Redirect("/Albums/All");
        }

        public HttpResponse Details(string id)
        {
            if (!IsUserLoggedIn())
            {
                return Redirect("/Users/Login");
            }

            var album = albumsService.GetDetails(id);
            var viewModel = new AlbumDetailsViewModel
            {
                Id = album.Id,
                Name = album.Name,
                Price = album.Price,
                Cover = album.Cover,
                Tracks = album.Tracks.Select(x => new TrackInfoViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList()
            };

            return View(viewModel);
        }
    }
}
