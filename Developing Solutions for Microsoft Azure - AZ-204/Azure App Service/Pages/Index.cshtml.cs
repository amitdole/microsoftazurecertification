using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DemoApplication2.Models;
using DemoApplication2.Services;

namespace DemoApplication2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IProductService _productService;
        public List<Product> Products;

        public IndexModel(ILogger<IndexModel> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public void OnGet()
        {
            Products = _productService.GetProducts().GetAwaiter().GetResult();
        }
    }
}