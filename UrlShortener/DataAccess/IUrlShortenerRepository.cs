using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UrlShortener.Entities;
using UrlShortener.Helpers;

namespace UrlShortener.DataAccess
{
    public interface IUrlShortenerRepository
    {
        void Add(ShortenedUrlEntity entity);

        IQueryable<ShortenedUrlEntity> Find(Expression<Func<ShortenedUrlEntity, bool>> predicate);

        IEnumerable<ShortenedUrlEntity> FindAll(UrlResourceParameter urlResourceParameter);

        ShortenedUrlEntity FindById(int id);

        void Remove(ShortenedUrlEntity entity);

        bool Save();
    }
}
