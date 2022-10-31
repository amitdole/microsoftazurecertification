using DemoApplication2.Models;

namespace DemoApplication2.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}