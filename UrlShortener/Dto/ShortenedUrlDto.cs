namespace UrlShortener.Dto
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

        [Display(Name = "Creation date")]
        public DateTime CreationDate { get; set; }
    }
}
