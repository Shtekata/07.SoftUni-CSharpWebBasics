using System.Collections.Generic;

namespace IRunes.Models.ViewModels
{
    public class AlbumDetailsDataModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Cover { get; set; }

        public IEnumerable<TrackInfoDataModel> Tracks { get; set; }
    }
}
