namespace UrlShortenerClient.Models
{
    using System.Collections.Generic;

    public class UrlShortenerViewModel
    {
        public List<ShortenedUrlDto> ShortenedUrlDtos { get; set; }

        public ShortenedUrlCreateDto ShortenedUrlCreateDto { get; set; }
    }
}
