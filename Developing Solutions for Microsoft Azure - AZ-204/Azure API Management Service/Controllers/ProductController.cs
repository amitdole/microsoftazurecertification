using DemoApplication2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Azure_WebApi_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        [HttpGet]
        public IActionResult GetProducts()
        {
            var productList = new List<Product>();

            productList.Add(new Product { ID = 1, Name = "IPad", Quantity = 2 });
            productList.Add(new Product { ID = 2, Name = "IPhone", Quantity = 4 });

            return Ok(productList);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var productList = new List<Product>();

            productList.Add(new Product { ID = 1, Name = "IPad", Quantity = 2 });
            productList.Add(new Product { ID = 2, Name = "IPhone", Quantity = 4 });

            return Ok(productList.Where(i => i.ID == id));
        }
    }
}
