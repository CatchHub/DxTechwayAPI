using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ProductListApp.Domain.Entities;

namespace ProductAppAPI.ServiceContracts
{
    public interface IProductsService
    {
        IEnumerable<Product> GetProducts();
        Task<Product> GetProduct(Guid id);

        Task<Product> PostProduct(Product product);

        Task UpdateProduct(string id, Product product);

        Task DeleteProduct(Guid id);

    }
}
