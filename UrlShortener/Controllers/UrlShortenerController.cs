using UrlShortener.Helpers;

namespace UrlShortener.Controllers
{
    using AutoMapper;
    using DataAccess;
    using Dto;
    using Entities;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Util;

    public class UrlShortenerController : Controller
    {
        private readonly IUrlShortenerRepository _urlShortenerRepository;

        public UrlShortenerController(IUrlShortenerRepository urlShortenerRepository)
        {
            _urlShortenerRepository = urlShortenerRepository;
        }

        [HttpGet("api/UrlShortener")]
        public IActionResult Get(UrlResourceParameter urlResourceParameter)
        {
            var urlsEntities = _urlShortenerRepository.FindAll(urlResourceParameter);
            var urlDtos = Mapper.Map<IEnumerable<ShortenedUrlDto>>(urlsEntities);
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

        [HttpGet("/{route}")]
        public IActionResult Get(string route)
        {
            var id = Shortener.RecoverId(route);
            var realUrl = _urlShortenerRepository.FindById(id);
            if (realUrl == null)
            {
                return NotFound();
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
