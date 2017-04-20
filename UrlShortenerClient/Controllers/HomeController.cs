namespace UrlShortenerClient.Controllers
{
    using Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly string endpoint = "http://localhost:61796/api/UrlShortener";

        [HttpGet(Name = "Home")]
        public async Task<IActionResult> Index(UrlResourceParameter urlResourceParameter)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                var query = $"{this.endpoint}/?PageNumber={urlResourceParameter.PageNumber}&pageSize={urlResourceParameter.PageSize}";
                response = await client.GetAsync(query);
            }

            if (!response.IsSuccessStatusCode)
            {
                this.ModelState.AddModelError("", response.ReasonPhrase);
            }

            var content = await response.Content.ReadAsStringAsync();
            var shUrlList = JsonConvert.DeserializeObject<List<ShortenedUrlDto>>(content);
            ViewData["shUrlList"] = shUrlList;

            var pagination = response.Headers.GetValues("X-Pagination");
            var pagJson = JsonConvert.DeserializeObject<Dictionary<string, string>>(pagination.FirstOrDefault());

            pagJson.Add("nextPage",int.Parse(pagJson["currentPage"]) < int.Parse(pagJson["totalPages"])
                ? $"{int.Parse(pagJson["currentPage"]) + 1}"
                : "#");

            pagJson.Add("previousPage", int.Parse(pagJson["currentPage"]) > 1
                ? $"{int.Parse(pagJson["currentPage"]) - 1}"
                : "#");

            ViewData["pagination"] = pagJson;

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> ShortenUrl(ShortenedUrlCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return this.View("Index", model);
            }

            HttpResponseMessage response = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                var jsonModel = JsonConvert.SerializeObject(model);
                for (int i = 0; i < 5000; i++)
                {
                    response = await client.PostAsync($"{this.endpoint}", new StringContent(jsonModel, Encoding.UTF8, "application/json"));
                }
            }

            if (!response.IsSuccessStatusCode)
            {
                this.ModelState.AddModelError("", response.ReasonPhrase);
                return this.View("Index", model);
            }

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                response = await client.DeleteAsync($"{this.endpoint}/{id}");
            }

            if (!response.IsSuccessStatusCode)
            {
                this.ModelState.AddModelError("", response.ReasonPhrase);
            }

            return this.RedirectToAction("Index");
        }
    }
}
