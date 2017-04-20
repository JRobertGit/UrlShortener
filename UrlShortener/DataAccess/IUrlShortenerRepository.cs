namespace UrlShortener.DataAccess
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Entities;
    using Helpers;

    public interface IUrlShortenerRepository
    {
        void Add(ShortenedUrlEntity entity);

        IQueryable<ShortenedUrlEntity> Find(Expression<Func<ShortenedUrlEntity, bool>> predicate);

        PageList<ShortenedUrlEntity> FindAll(UrlResourceParameter urlResourceParameter);

        ShortenedUrlEntity FindById(int id);

        void Remove(ShortenedUrlEntity entity);

        void Update(ShortenedUrlEntity entity);

        bool Save();
    }
}
