namespace UrlShortener.Dto
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Util;

    public class ShortenedUrlDto
    {
        public int Id { get; set; }

        [Display(Name = "Url")]
        public string Url { get; set; }

        private string _shortenedUrl;

        [Display(Name = "Shortened Url")]
        public string ShortenedUrl => _shortenedUrl ?? (_shortenedUrl = "http://ushapi.azurewebsites.net" + Shortener.ShortenUrl(Id));

        [Display(Name = "Creation date")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Clicks")]
        public int Clicks { get; set; }
    }
}
