namespace UrlShortener.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ShortenedUrlEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Url]
        [Required]
        public string Url { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
        
        public int Clicks { get; set; }

        public DateTime LastVisit { get; set; }
    }
}
