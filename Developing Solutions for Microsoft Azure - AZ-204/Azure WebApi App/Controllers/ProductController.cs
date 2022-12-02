using DemoApplication2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Azure_WebApi_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ProductService productService;

        public ProductController()
        {
            productService = new ProductService();
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(productService.GetProducts());
        }

        //[HttpGet("{id}")]
        //public IActionResult GetProduct(string id)
        //{
        //    return Ok(productService.GetProduct(id));
        //}
    }
}
