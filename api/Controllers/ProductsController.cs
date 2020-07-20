using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastucture.Data;
using Core.Entities;
using Core.Interfaces;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;
        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts(CancellationToken cancellationToken)
        {
            var products = await _repo.GetPrductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id, CancellationToken cancellationToken)
        {
            var product = await _repo.GetProductByIdAsync(id);
            return product;
        }

        [HttpGet("brands")]
        public async Task<ActionResult<ProductBrand>> GetProductBrands()
        {
            return Ok(await _repo.GetProductBrandsAsync());
        }


        [HttpGet("Types")]
        public async Task<ActionResult<ProductType>> GetProductTypes()
        {
            return Ok(await _repo.GetPrductTypesAsync());
        }


    }
}