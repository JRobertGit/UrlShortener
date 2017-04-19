using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UrlShortenerClient.Models;

namespace UrlShortenerClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly string endpoint = "http://localhost:61796/api/UrlShortener";

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                response = await client.GetAsync($"{this.endpoint}");
            }

            if (!response.IsSuccessStatusCode)
            {
                this.ModelState.AddModelError("", response.ReasonPhrase);
            }

            var content = await response.Content.ReadAsStringAsync();
            var shUrlList = JsonConvert.DeserializeObject<List<ShortenedUrlDto>>(content);
            ViewData["shUrlList"] = shUrlList;

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

            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                var jsonModel = JsonConvert.SerializeObject(model);
                response = await client.PostAsync($"{this.endpoint}", new StringContent(jsonModel, Encoding.UTF8, "application/json"));
            }

            if (!response.IsSuccessStatusCode)
            {
                this.ModelState.AddModelError("", response.ReasonPhrase);
                return this.View("Index", model);
            }

            return this.RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
