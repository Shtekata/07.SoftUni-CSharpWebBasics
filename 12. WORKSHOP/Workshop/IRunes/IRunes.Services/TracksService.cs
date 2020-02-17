using IRunes.Data;
using IRunes.Models;
using IRunes.Models.ViewModels;
using System.Linq;

namespace IRunes.Services
{
    public class TracksService : ITracksService
    {
        private readonly RunesDbContext db;

        public TracksService(RunesDbContext db)
        {
            this.db = db;
        }

        public void Create(string albumId, string name, string link, decimal price)
        {
            var track = new Track
            {
                AlbumId = albumId,
                Name = name,
                Link = link,
                Price = price,
            };

            db.Tracks.Add(track);
            var allTrackPricesSum = db.Tracks
                .Where(x => x.AlbumId == albumId)
                .Sum(x => x.Price) + price;
            var album = db.Albums.Find(albumId);
            album.Price = allTrackPricesSum * 0.87m;

            db.SaveChanges();
        }

        public TrackDetailsDataModel GetTrack(string trackId)
        {
            var track = db.Tracks.FirstOrDefault(x => x.Id == trackId);

            return new TrackDetailsDataModel
            {
                Name = track.Name,
                Link = track.Link,
                Price = track.Price,
            };
        }
    }
}
