﻿namespace UrlShortenerClient.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ShortenedUrlDto
    {
        public int Id { get; set; }

        [Display(Name = "Url")]
        public string Url { get; set; }

        [Display(Name = "Shortened Url")]
        public string ShortenedUrl { get; set; }

        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Clicks")]
        public int Clicks { get; set; }

        [Display(Name = "Last Visit")]
        public DateTime LastVisit { get; set; }
    }
}
