using ProductAppAPI.Mappers;
using ProductListApp.Domain.Entities;
using ProductWebAPI.DTOs;

namespace ProductWebAPI.Mappers.ProductsMappers
{
    public class ProductsRequestMapper : IMapper<ProductsDTO, Product>
    {
        public Product Map(ProductsDTO entity)
        {
            return new()
            {
                Name = entity.Name,
                Stock = entity.Stock,
                ProductCode = entity.ProductCode,
                Price = entity.Price,
                Detail = entity.Detail,
                CurrencyCode = entity.CurrencyCode,
                Brand = entity.Brand,
                Id = entity.Id
            };
        }

    }
}
