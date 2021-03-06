﻿namespace MeTube.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Tube
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        public string Description { get; set; }

        [Required]
        public string YoutubeId { get; set; }

        public int Views { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}