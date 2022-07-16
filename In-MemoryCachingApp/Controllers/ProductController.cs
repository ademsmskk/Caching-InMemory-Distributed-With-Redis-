using In_MemoryCachingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace In_MemoryCachingApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMemoryCache _memoryCache;
        public ProductController(IMemoryCache memoryCache)
        {
             _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
           
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
                options.AbsoluteExpiration= DateTimeOffset.Now.AddSeconds(60);
                options.SlidingExpiration = TimeSpan.FromSeconds(10);
                options.Priority = CacheItemPriority.Low;
                _memoryCache.Set<string>("zaman", DateTime.Now.ToString(),options);

                Product P =new Product{ Id=1,Name="Kalem", Price=2};
                _memoryCache.Set<Product>("product",P);

                return View();
        }

        public IActionResult Show()
        {
             _memoryCache.TryGetValue("zaman", out string zamancache);
             ViewBag.zaman= zamancache;
             ViewBag.product = _memoryCache.Get<Product>("product");
             return View();
          
        }
    }
}
