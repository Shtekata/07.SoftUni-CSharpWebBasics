using IRunes.Models.ViewModels;

namespace IRunes.Services
{
    public interface ITracksService
    {
        void Create(string albumId, string name, string linq, decimal price);

        TrackDetailsDataModel GetTrack(string trackId);
    }
}
