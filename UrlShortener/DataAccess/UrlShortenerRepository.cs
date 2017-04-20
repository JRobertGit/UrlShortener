using UrlShortener.Helpers;

namespace UrlShortener.DataAccess
{
    using DbContext;
    using Entities;
    using System;
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

        public PageList<ShortenedUrlEntity> FindAll(UrlResourceParameter urlResourceParameter)
        {
            var urls = _urlShortenerContext.ShortenedUrls.
                OrderByDescending(u => u.CreationDate);
            return PageList<ShortenedUrlEntity>.Create(urls,
                urlResourceParameter.PageNumber,
                urlResourceParameter.PageSize);
        }

        public ShortenedUrlEntity FindById(int id)
        {
            return _urlShortenerContext.ShortenedUrls.FirstOrDefault(u => u.Id == id);
        }

        public void Remove(ShortenedUrlEntity entity)
        {
            _urlShortenerContext.ShortenedUrls.Remove(entity);
        }

        public void Update(ShortenedUrlEntity entity)
        {
            _urlShortenerContext.ShortenedUrls.Update(entity);
        }

        public bool Save()
        {
            return _urlShortenerContext.SaveChanges() >= 0;
        }
    }
}
