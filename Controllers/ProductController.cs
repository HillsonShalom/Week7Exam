using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Week7Exam.Models;

namespace Week7Exam.Controllers
{
    public class ProductController : Controller
    {
        private HttpClient _httpClient;
        public ProductController()
        {
            _httpClient = new HttpClient();
        }
        public async Task<IActionResult> Index()
        {
            string json = await _httpClient.GetStringAsync("https://fakestoreapi.com/products");
            List<Product> list = JsonConvert.DeserializeObject<List<Product>>(json);
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            try
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(product));
                var res  = await _httpClient.PostAsync("https://fakestoreapi.com/products", content);
                Console.WriteLine(res.StatusCode);
            }
            catch (Exception ex)
            {
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}
