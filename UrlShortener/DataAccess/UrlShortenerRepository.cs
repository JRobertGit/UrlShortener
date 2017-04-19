namespace UrlShortener.DataAccess
{
    using DbContext;
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class UrlShortenerRepository : IUrlShortenerRepository
    {
        private readonly UrlShortenerContext _urlShortenerContext;

        public UrlShortenerRepository(UrlShortenerContext context)
        {
            _urlShortenerContext = context;
        }

        public void Add(ShortenedUrlEntity entity)
        {
            _urlShortenerContext.Add(entity);
        }

        public IQueryable<ShortenedUrlEntity> Find(Expression<Func<ShortenedUrlEntity, bool>> predicate)
        {
            return _urlShortenerContext.ShortenedUrls.Where(predicate);
        }

        public IEnumerable<ShortenedUrlEntity> FindAll()
        {
            return _urlShortenerContext.ShortenedUrls.OrderBy(u => u.Url).ToList();
        }

        public ShortenedUrlEntity FindById(int id)
        {
            return _urlShortenerContext.ShortenedUrls.FirstOrDefault(u => u.Id == id);
        }

        public void Remove(ShortenedUrlEntity entity)
        {
            _urlShortenerContext.ShortenedUrls.Remove(entity);
        }

        public bool Save()
        {
            return _urlShortenerContext.SaveChanges() >= 0;
        }
    }
}
