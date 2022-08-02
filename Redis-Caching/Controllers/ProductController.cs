using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Redis_Caching.Models;
using System;

namespace Redis_Caching.Controllers
{
    public class ProductController : Controller
    {
        private readonly IDistributedCache _distributedCache;
        public ProductController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public IActionResult Index()
        {
            DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions();

            cacheEntryOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(1);
            _distributedCache.SetString("name", "Adem", cacheEntryOptions);
             Product product = new Product
             {
                Name ="Kalem",
                Price=20,
                Id = 1,
              
              };
            string prdct = JsonConvert.SerializeObject(product);
            _distributedCache.SetString("Product:1",prdct);

            return View();
        }
        public IActionResult Show()
        {
            string name =_distributedCache.GetString("name");
            ViewBag.Name = name;

            var b = _distributedCache.GetString("Product:1");
            var myvalue =  JsonConvert.DeserializeObject(b);
            ViewBag.Product=myvalue;
            return View();
        }
        public IActionResult Delete()
        {
            _distributedCache.Remove("name");
            return View();
        }
    }
}
