using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAppAPI.Entities;
using ProductAppAPI.Mappers;
using ProductAppAPI.ServiceContracts;
using ProductAppAPI.Services;
using ProductListApp.Domain.Entities;
using ProductListApp.Persistence.Contexts;
using ProductWebAPI.DTOs;
using ProductWebAPI.Mappers.ProductsMappers;

namespace ProductAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService productsService;
        private readonly IMapper<ProductsDTO, Product> productRequestMapper;
        private readonly IMapper<Product, ProductsDTO> productsResponseMapper;

        public ProductsController(IProductsService productsService, IMapper<ProductsDTO, Product> productRequestMapper ,IMapper<Product, ProductsDTO> productsResponseMapper)
        {
            this.productsService = productsService;
            this.productRequestMapper = productRequestMapper;
            this.productsResponseMapper = productsResponseMapper;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<ProductsDTO>> GetProducts()
        {
            try
            {
                var products = this.productsService.GetProducts();
                products.Select((product) =>
                    this.productsResponseMapper.Map(product)
                );
                return Ok(products);
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostProduct(ProductsDTO productDTO)
        {
            try
            {
                var product = this.productRequestMapper.Map(productDTO);
                var createdProduct = await productsService.PostProduct(product);
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductsDTO>> GetProduct(Guid id)
        {
            try
            {
                var result = await productsService.GetProduct(id);
                var resultDTO = this.productsResponseMapper.Map(result);
                return Ok(resultDTO);
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(string id, ProductsDTO productDTO)
        {
            try
            {
                var product = this.productRequestMapper.Map(productDTO);
                await productsService.UpdateProduct(id, product);
                return Ok(product);
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                await productsService.DeleteProduct(id);
                
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(new { message = exception.Message });
            }
        }
    }
}
