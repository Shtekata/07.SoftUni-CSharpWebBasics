using IRunes.Data;
using IRunes.Models;
using IRunes.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IRunes.Services
{
    public class AlbumsService : IAlbumsService
    {
        private readonly RunesDbContext db;

        public AlbumsService(RunesDbContext db)
        {
            this.db = db;
        }
        public void Create(string name, string cover)
        {
            var album = new Album
            {
                Name = name,
                Cover = cover,
                Price = 0m,
            };
            db.Albums.Add(album);
            db.SaveChanges();
        }

        public IEnumerable<AlbumInfoDataModel> GetAll()
        {
            var allAlbums = db.Albums.Select(x => new AlbumInfoDataModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return allAlbums;
        }

        public IEnumerable<T> GetAllNew<T>(Func<Album, T> selectFunc)
        {
            var allAlbums = db.Albums.Select(selectFunc).ToList();
            return allAlbums;
        }

        public AlbumDetailsDataModel GetDetails(string id)
        {
            var album = db.Albums.Where(x => x.Id == id)
                .Select(x => new AlbumDetailsDataModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Cover = x.Cover,
                    Tracks = x.Tracks.Select(y => new TrackInfoDataModel
                    {
                        Id = y.Id,
                        Name = y.Name,
                    })
                }).FirstOrDefault();
            return album;
                
        }
    }
}
