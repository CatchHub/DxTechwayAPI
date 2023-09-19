using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ProductAppAPI.Entities;
using ProductAppAPI.ServiceContracts;
using ProductListApp.Domain.Entities;
using ProductListApp.Persistence.Contexts;

namespace ProductAppAPI.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ProductListAppDbContext context;

        public ProductsService(ProductListAppDbContext dbContext)
        {
            this.context = dbContext;
        }


        public IEnumerable<Product> GetProducts()
        {
            return this.context.Products.Where((product)=>!product.IsDeleted);
        }

        public async Task<Product> GetProduct(Guid id)
        {
            var product = await context.Products.FindAsync(id);
            if(product == null || product.IsDeleted)
            {
                throw new Exception();
            }
            return product;
        }


        public async Task<Product> PostProduct(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();
            return product;
        }
        public async Task UpdateProduct(string id, Product product)
        {
            if (!Guid.TryParse(id, out Guid guidID))
            {
                throw new Exception();
            }
            var oldProduct = await context.Products.FindAsync(guidID);
            if (oldProduct == null || oldProduct.IsDeleted)
            {
                throw new Exception();
            }
            oldProduct.Name = product.Name;
            oldProduct.Brand = product.Brand;
            oldProduct.Price = product.Price;
            oldProduct.CurrencyCode = product.CurrencyCode;
            oldProduct.Stock = product.Stock;
            oldProduct.ProductCode = product.ProductCode;
            oldProduct.Detail = product.Detail;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(guidID))
                {
                    throw new Exception();
                }
                else
                {
                    throw;
                }
            }
        }



        public async Task DeleteProduct(Guid id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null)
            {
                throw new Exception();
            }
            product.IsDeleted = true;
            await context.SaveChangesAsync();
        }

        private bool ProductExists(Guid id)
        {
            return context.Products.Any(e => e.Id == id);
        }
    }
}
