using System.Linq;
using Microsoft.Extensions.Options;

namespace UrlShortener.Controllers
{
    using AutoMapper;
    using DataAccess;
    using Dto;
    using Entities;
    using Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using Util;

    public class UrlShortenerController : Controller
    {
        private readonly IUrlShortenerRepository _urlShortenerRepository;

        private readonly ApiOptions _options;

        public UrlShortenerController(IUrlShortenerRepository urlShortenerRepository, IOptions<ApiOptions> options)
        {
            _urlShortenerRepository = urlShortenerRepository;
            _options = options.Value;
        }

        [HttpGet("api/UrlShortener", Name = "GetUrls")]
        public IActionResult Get(UrlResourceParameter urlResourceParameter)
        {
            var urls = _urlShortenerRepository.FindAll(urlResourceParameter);

            var paginationMetaData = new
            {
                totalCount = urls.TotalCount,
                pageSize = urls.PageSize,
                currentPage = urls.CurrentPage,
                totalPages = urls.TotalPages
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetaData));

            var urlDtos = urls.Select(u => new ShortenedUrlDto(_options.Domain)
            {
                Id = u.Id,
                Url = u.Url,
                CreationDate = u.CreationDate,
                Clicks = u.Clicks,
                LastVisit = u.LastVisit
            });

            return Ok(urlDtos);
        }

        [HttpGet("api/UrlShortener/{id}", Name = "GetShortenedUrl")]
        public IActionResult Get(int id)
        {
            var urlEntity = _urlShortenerRepository.FindById(id);
            if (urlEntity == null)
            {
                return NotFound();
            }

            var urlDto = Mapper.Map<ShortenedUrlDto>(urlEntity);
            return Ok(urlDto);
        }

        [HttpGet("/{route}", Name = "RedirectToUrl")]
        public IActionResult Get(string route)
        {
            var id = Shortener.RecoverId(route);
            var realUrl = _urlShortenerRepository.FindById(id);
            if (realUrl == null)
            {
                return NotFound();
            }

            realUrl.Clicks++;
            realUrl.LastVisit = DateTime.Now;
            _urlShortenerRepository.Update(realUrl);

            if (!_urlShortenerRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request");
            }

            return RedirectPermanent(realUrl.Url);
        }

        // POST api/values
        [HttpPost("api/UrlShortener")]
        public IActionResult Post([FromBody]ShortenedUrlCreateDto value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUrl = Mapper.Map<ShortenedUrlEntity>(value);
            newUrl.CreationDate = DateTime.Now;

            _urlShortenerRepository.Add(newUrl);

            if (!_urlShortenerRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request");
            }

            return CreatedAtRoute("GetShortenedUrl", new { newUrl.Id}, newUrl);
        }

        // PUT api/values/5
        [HttpPut("api/UrlShortener/{id}")]
        public IActionResult Put(int id, [FromBody]ShortenedUrlUpdateDto value)
        {
            var url = _urlShortenerRepository.FindById(id);
            if (url == null)
            {
                return NotFound();
            }

            // Here logic to update url entity data with value parameter.
            // We do nothing because there is nothing yet to update.
            // _urlShortenerRepository.Update(url);

            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("api/UrlShortener/{id}")]
        public IActionResult Delete(int id)
        {
            var url = _urlShortenerRepository.FindById(id);
            if (url == null)
            {
                return NotFound();
            }

            _urlShortenerRepository.Remove(url);

            if (!_urlShortenerRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling the request.");
            }

            return NoContent();
        }
    }
}
