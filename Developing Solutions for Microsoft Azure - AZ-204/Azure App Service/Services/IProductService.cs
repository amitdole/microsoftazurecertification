using DemoApplication2.Models;

namespace DemoApplication2.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
    }
}