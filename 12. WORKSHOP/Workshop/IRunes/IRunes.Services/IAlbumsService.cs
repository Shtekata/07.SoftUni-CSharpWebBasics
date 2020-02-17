using System;
using System.Collections.Generic;
using IRunes.Models;
using IRunes.Models.ViewModels;

namespace IRunes.Services
{
    public interface IAlbumsService
    {
        void Create(string name, string cover);

        IEnumerable<AlbumInfoDataModel> GetAll();

        IEnumerable<T> GetAllNew<T>(Func<Album, T> selectFunc);

        AlbumDetailsDataModel GetDetails(string id);
    }
}
