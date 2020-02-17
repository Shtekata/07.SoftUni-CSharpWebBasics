using System.Collections.Generic;

namespace IRunes.App.ViewModels.Albums
{
    public class AlbumDetailsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Cover { get; set; }

        public IList<TrackInfoViewModel> Tracks { get; set; }
    }
}
