namespace UrlShortener.Dto
{
    using System.ComponentModel.DataAnnotations;

    public class ShortenedUrlCreateDto
    {
        [Required(ErrorMessage = "Url field is required")]
        [Display(Name = "Url")]
        [Url(ErrorMessage = "Invalid Url")]
        public string Url { get; set; }
    }
}