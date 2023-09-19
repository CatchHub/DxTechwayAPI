using ProductAppAPI.Mappers;
using ProductListApp.Domain.Entities;
using ProductWebAPI.DTOs;

namespace ProductWebAPI.Mappers.ProductsMappers
{
    public class ProductsResponseMapper : IMapper<Product, ProductsDTO> 
    {
        public ProductsDTO Map(Product entity)
        {
            return new()
            {
                Id = entity.Id,
                Name = entity.Name,
                Brand = entity.Brand,
                CurrencyCode = entity.CurrencyCode,
                Detail = entity.Detail,
                Price = entity.Price,
                ProductCode = entity.ProductCode,
                Stock = entity.Stock
            };
        }
    }
}
