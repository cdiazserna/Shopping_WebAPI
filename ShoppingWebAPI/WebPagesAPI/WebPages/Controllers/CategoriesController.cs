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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var url = "https://localhost:7007/api/Categories/Get";
            var json = await _httpClient.CreateClient().GetStringAsync(url);
            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(json);
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        { 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            var url = "https://localhost:7007/api/Categories/Create";
            await _httpClient.CreateClient().PostAsJsonAsync(url, category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            return View(await GetCategoriesById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, Category category)
        {
            var url = String.Format("https://localhost:7007/api/Categories/Edit/{0}", id);
            await _httpClient.CreateClient().PutAsJsonAsync(url, category);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            return View(await GetCategoriesById(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var url = String.Format("https://localhost:7007/api/Categories/Delete/{0}", id);
            await _httpClient.CreateClient().DeleteAsync(url);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            return View(await GetCategoriesById(id));
        }

        private async Task<Category> GetCategoriesById(Guid? id)
        {
            var url = String.Format("https://localhost:7007/api/Categories/Get/{0}", id);
            return JsonConvert.DeserializeObject<Category>(await _httpClient.CreateClient().GetStringAsync(url));
        }
    }
}
