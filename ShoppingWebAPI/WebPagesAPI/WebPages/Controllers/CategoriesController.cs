using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoppingWebAPI.DAL.Entities;
using System.Text.Json.Serialization;

namespace WebPages.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        public CategoriesController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var url = "https://localhost:7007/api/Categories/Get";
            var json = await _httpClient.CreateClient().GetStringAsync(url);
            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(json);
            return View(categories);
        }
    }
}
