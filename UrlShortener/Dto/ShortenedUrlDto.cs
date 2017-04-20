namespace UrlShortener.Dto
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Util;

    public class ShortenedUrlDto
    {
        private const string DomainTest = "http://localhost:61796/";
        //private const string DomainProd = "http://ushapi.azurewebsites.net/";

        public int Id { get; set; }

        [Display(Name = "Url")]
        public string Url { get; set; }

        private string _shortenedUrl;

        [Display(Name = "Shortened Url")]
        public string ShortenedUrl => _shortenedUrl ?? (_shortenedUrl = $"{DomainTest}{Shortener.ShortenUrl(Id)}");

        [Display(Name = "Creation date")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Clicks")]
        public int Clicks { get; set; }

        [Display(Name = "Last Visit")]
        public DateTime LastVisit { get; set; }
    }
}
