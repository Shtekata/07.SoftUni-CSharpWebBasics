using System;
using System.ComponentModel.DataAnnotations;

namespace IRunes.Models
{
    public class Track
    {
        public Track()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public string Link { get; set; }

        public decimal Price { get; set; }

        [Required]
        public string AlbumId { get; set; }

        public Album Album { get; set; }
    }
}
