namespace UrlShortener.DataAccess.DbContext
{
    using Entities;
    using Microsoft.EntityFrameworkCore;
    public sealed class UrlShortenerContext : DbContext
    {
        public UrlShortenerContext(DbContextOptions<UrlShortenerContext> options) 
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<ShortenedUrlEntity> ShortenedUrls { get; set; }
    }
}
